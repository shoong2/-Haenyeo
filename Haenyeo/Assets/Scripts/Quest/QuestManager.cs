using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [Header("퀘스트 선택 창")]
    [SerializeField] TMP_Text questText;

    [Header("퀘스트 디테일")]
    [SerializeField] TMP_Text questDetailName;
    [SerializeField] TMP_Text questDetail;

    Quest[] quests;

    [Header("저장소")]
    [SerializeField] SaveNLoad storage;

    [SerializeField] GameObject QuestBoxList;
    [SerializeField] GameObject questBoxPrefab;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveQuest()
    {
        int qCount = storage.saveData.questAllCount;
        int qCompleteCount = storage.saveData.completeQuest;

        while(QuestBoxList.transform.childCount != qCount - qCompleteCount)
        {
            GameObject temp = Instantiate(questBoxPrefab);
            temp.transform.SetParent(QuestBoxList.transform);
        }

        quests = DatabaseManager.instance.GetQuest(qCount - qCompleteCount, qCount);
        //if (storage.saveData.nowIndex == 1)
        //{
        //    //quests = DatabaseManager.instance.GetQuest(1, 1);
           
        //    string questName = quests[storage.saveData.questNowCount].name;
        //    string questDetailText = quests[storage.saveData.questNowCount].details[0];

        //    questDetailText = questDetailText.Replace("'", ",");
        //    questDetailText = questDetailText.Replace("\\n", "\n");


        //    questText.text = questName;
        //    questDetailName.text = questName;
        //    questDetail.text = questDetailText;

        //}

        for(int i =0; i<QuestBoxList.transform.childCount; i++)
        {

        }
    }
}
