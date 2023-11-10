using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    #region Save Path
    const string KsaveRootPath = "questSystem";
    const string KActiveQuestsSavePath = "activeQuests";
    const string KCompletedQuestsSavePath = "completedQuests";
    const string KActiveAchievementsSavePath = "activeAchievement";
    const string KCompletedAchievementsSavePath = "completedAchievement";
    #endregion

    #region Events
    public delegate void QuestReigisteredHandler(Quest_ newQuest);
    public delegate void QuestCompletedHandler(Quest_ quest);
    public delegate void QuestCanceledHandler(Quest_ quest);
    #endregion

    static QuestSystem instance;
    static bool isApplicationQuitting;

    public static QuestSystem Instance
    {
        get
        {
            if(!isApplicationQuitting && instance ==null)
            {
                instance = FindObjectOfType<QuestSystem>();
                if(instance == null)
                {
                    instance = new GameObject("Quest System").AddComponent<QuestSystem>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }

    List<Quest_> activeQuests = new List<Quest_>();
    List<Quest_> completedQuests = new List<Quest_>();

    List<Quest_> activeAchievements = new List<Quest_>();
    List<Quest_> completedAchievements = new List<Quest_>();

    QuestDatabase questDatabase;
    QuestDatabase achievementDatabase;

    public event QuestReigisteredHandler onQuestRegistered;
    public event QuestCompletedHandler onQuestCompleted;
    public event QuestCanceledHandler onQuestCanceled;

    public event QuestReigisteredHandler onAchievementRegistered;
    public event QuestCompletedHandler onAchievementCompleted;
    public IReadOnlyList<Quest_> ActiveQuests => activeQuests;
    public IReadOnlyList<Quest_> CompletedQuests => completedQuests;
    public IReadOnlyList<Quest_> ActiveAchievements => activeAchievements;
    public IReadOnlyList<Quest_> CompletedAchievements => completedAchievements;

    private void Awake()
    {
        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        achievementDatabase = Resources.Load<QuestDatabase>("AchievementDatabase");

        if(!Load())
        {
            foreach (var achievement in achievementDatabase.Quests)
                Register(achievement);
        }
    }

    private void OnApplicationQuit()
    {
        isApplicationQuitting = true;
        Save();
    }

    public Quest_ Register(Quest_ quest)
    {
        var newQuest = quest.Clone();

        if(newQuest is Achievement)
        {
            newQuest.onCompleted += OnAchievementCompleted;

            activeAchievements.Add(newQuest);

            newQuest.OnRegister();
            onAchievementRegistered?.Invoke(newQuest);
        }
        else
        {
            newQuest.onCompleted += OnQuestCompleted;
            newQuest.onCanceled += OnQuestCanceled;

            activeQuests.Add(newQuest);

            newQuest.OnRegister();
            onQuestRegistered?.Invoke(newQuest);
        }

        return newQuest;
    }

    public void ReceiveReport(string category, object target, int successCount)
    {
        ReceiveReport(activeQuests, category, target, successCount);
        ReceiveReport(activeAchievements, category, target, successCount);
    }

    public void ReceiveReport(Category category, TaskTarget target, int successCount)
        => ReceiveReport(category.CodeName, target.Value, successCount);

    void ReceiveReport(List<Quest_> quests, string category, object target, int successCount)
    {
        foreach (var quest in quests.ToArray())
            quest.ReceiveReport(category, target, successCount);
    }

    public bool ContainsInActiveQuests(Quest_ quest) => activeQuests.Any(x => x.CodeName == quest.CodeName);
    public bool ContainsInCompleteQuests(Quest_ quest) => completedQuests.Any(x => x.CodeName == quest.CodeName);
    public bool ContainsInActiveAchievements(Quest_ quest) => activeAchievements.Any(x => x.CodeName == quest.CodeName);
    public bool ContainsInCompletedAchievements(Quest_ quest) => completedAchievements.Any(x => x.CodeName == quest.CodeName);

    void Save()
    {
        var root = new JObject();
        root.Add(KActiveQuestsSavePath, CreatSaveDatas(activeQuests));
        root.Add(KCompletedQuestsSavePath, CreatSaveDatas(completedQuests));
        root.Add(KActiveAchievementsSavePath, CreatSaveDatas(activeAchievements));
        root.Add(KCompletedAchievementsSavePath, CreatSaveDatas(completedAchievements));

        PlayerPrefs.SetString("questSystem", root.ToString());
        PlayerPrefs.Save();
    }

    bool Load()
    {
        if (PlayerPrefs.HasKey(KsaveRootPath))
        {
            var root = JObject.Parse(PlayerPrefs.GetString(KsaveRootPath));

            LoadSaveDatas(root[KActiveQuestsSavePath], questDatabase, LoadActiveQuest);
            LoadSaveDatas(root[KCompletedQuestsSavePath], questDatabase, LoadCompletedQuest);

            LoadSaveDatas(root[KActiveAchievementsSavePath], achievementDatabase, LoadActiveQuest);
            LoadSaveDatas(root[KCompletedAchievementsSavePath], achievementDatabase, LoadCompletedQuest);

            return true;
        }
        else
            return false;
    }
    JArray CreatSaveDatas(IReadOnlyList<Quest_> quests)
    {
        var saveDatas = new JArray();
        foreach(var quest in quests)
        {
            if(quest.IsSavable)
                saveDatas.Add(JObject.FromObject(quest.ToSaveData()));
        }
        return saveDatas;
    }

    void LoadSaveDatas(JToken datasToken, QuestDatabase database, System.Action<QuestSaveData, Quest_> onSuccess)
    {
        var datas = datasToken as JArray;
        foreach(var data in datas)
        {
            var saveData = data.ToObject<QuestSaveData>();
            var quest = database.FindQuestsBy(saveData.codeName);
            onSuccess.Invoke(saveData, quest);
        }
    }

    void LoadActiveQuest(QuestSaveData saveData, Quest_ quest)
    {
        var newQuest = Register(quest);
        newQuest.LoadFrom(saveData);
    }

    void LoadCompletedQuest(QuestSaveData saveData, Quest_ quest)
    {
        var newQuest = quest.Clone();
        newQuest.LoadFrom(saveData);

        if (newQuest is Achievement)
            completedAchievements.Add(newQuest);
        else
            completedQuests.Add(newQuest);
    }

    #region Callback
    void OnQuestCompleted(Quest_ quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        onQuestCompleted.Invoke(quest);
    }

    void OnQuestCanceled(Quest_ quest)
    {
        activeQuests.Remove(quest);
        onQuestCanceled?.Invoke(quest);

        Destroy(quest, Time.deltaTime);
    }

    void OnAchievementCompleted(Quest_ achievement)
    {
        activeAchievements.Remove(achievement);
        completedAchievements.Add(achievement);

        onAchievementCompleted?.Invoke(achievement);
    }
    #endregion
}
