using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Quest/QuestDatabase")]
public class QuestDatabase : ScriptableObject
{
    [SerializeField]
    List<Quest_> quests;

    public IReadOnlyList<Quest_> Quests => quests;

    public Quest_ FindQuestsBy(string codeName) => quests.FirstOrDefault(x => x.CodeName == codeName);

#if UNITY_EDITOR
    [ContextMenu("FindQuests")]
    void FindQuests()
    {
        FindQuestsBy<Quest_>();
    }

    [ContextMenu("FindAchievements")]
    void FindAchievements()
    {
        FindQuestsBy<Achievement>();
    }

    void FindQuestsBy<T>() where T: Quest_
    {
        quests = new List<Quest_>();

        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
        foreach( var guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var quest = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            if (quest.GetType() == typeof(T))
                quests.Add(quest);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }

#endif

}
