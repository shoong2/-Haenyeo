using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;


    //퀘스트 테스트
    [SerializeField] string csv_QuestFileName;

    Dictionary<int, Quest> questDic = new Dictionary<int, Quest>();

    public SaveNLoad storage;

    //reward
    [SerializeField] string csv_RewardFileName;

    Dictionary<int, Reward_> rewardDic = new Dictionary<int, Reward_>();


    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
            DialogueParser theParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName);
            for(int i=0; i<dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]);
            }
            isFinish = true;

            QuestParser QParser = GetComponent<QuestParser>();
            Quest[] quests = QParser.Parse(csv_QuestFileName);
            for(int j=0; j<quests.Length; j++)
            {
                questDic.Add(int.Parse(quests[j].questIndex), quests[j]);

            }

            RewardParser RParser = GetComponent<RewardParser>();
            Reward_[] rewards = RParser.Parse(csv_RewardFileName);
            for(int q=0; q<rewards.Length; q++)
            {
                rewardDic.Add(q + 1, rewards[q]);
            }
        }
    }

    //public Dialogue[] GetDialogue(int _StartNum, int _EndNum)
    //{
    //    List<Dialogue> dialogueList = new List<Dialogue>();

    //    for(int i=0; i<=_EndNum -_StartNum; i++)
    //    {
    //        dialogueList.Add(dialogueDic[_StartNum + i]);
    //    }

    //    return dialogueList.ToArray();
    //}

    public Dialogue[] GetDialogue(int index)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        dialogueList.Add(dialogueDic[index+1]);
        return dialogueList.ToArray();
    }

    public Quest[] GetQuest(int questCount)
    {
        List<Quest> questList = new List<Quest>();
        if(questDic.ContainsKey(questCount))
            questList.Add(questDic[questCount]);
        return questList.ToArray();
    }

    public Reward_[] GetReward(int index)
    {
        List<Reward_> rewardList = new List<Reward_>();
        rewardList.Add(rewardDic[index + 1]);
        return rewardList.ToArray();
    }

}
