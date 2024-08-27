using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{

    [SerializeField]
    Quest[] quests;


    private void Start()
    {
        foreach(var quest in quests)
        {
            if (quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompletedQuests(quest))
                QuestSystem.Instance.Register(quest);
        }

        var questSystem = QuestSystem.Instance;
        questSystem.onQuestCompleted += GiveQuest;
    }


    void GiveQuest(Quest quest_)
    {
        foreach (var quest in quests)
        {
            Debug.Log(quest.IsComplete);
            Debug.Log(quest.IsAcceptable);
            Debug.Log(QuestSystem.Instance.ContainsInCompletedQuests(quest));
            if (!quest.IsComplete && quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompletedQuests(quest))
            {
                Debug.Log("registerd");
                QuestSystem.Instance.Register(quest);
            }
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("click");
            foreach (var quest in quests)
            {
                Debug.Log(quest.IsComplete);
                Debug.Log(quest.IsAcceptable);
                Debug.Log(QuestSystem.Instance.ContainsInCompletedQuests(quest));
                if (!quest.IsComplete && quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompletedQuests(quest))
                {
                    Debug.Log("registerd");
                    QuestSystem.Instance.Register(quest);
                }
            }
        }

    }

}
