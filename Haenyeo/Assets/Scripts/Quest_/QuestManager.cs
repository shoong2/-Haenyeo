using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{

    [SerializeField] GameObject QuestBoxList;
    [SerializeField] GameObject[] questBox;

    [Header("퀘스트 선택 창")]
    [SerializeField] TMP_Text questText;

    [Header("퀘스트 디테일")]
    [SerializeField] GameObject[] questDetailList;

    Quest_[] quests;
    
    [Header("저장소")]
    [SerializeField] SaveNLoad storage;

    public static bool completeQuest = false;


    //private void Start()
    //{
    //    for(int i= storage.saveData.completeQuest; i< storage.saveData.questAllCount; i++)
    //    {
    //        quests = DatabaseManager.instance.GetQuest(storage.saveData.questAllCount);
    //        GameObject temp = Instantiate(questBoxPrefab);
    //        temp.transform.SetParent(QuestBoxList.transform);
    //        temp.transform.localScale = new Vector3(1, 1, 1);
    //        temp.transform.GetChild(0).GetComponent<TMP_Text>().text = quests[i].name;

    //        //0번 메인텍스트, 1번 디테일텍스트
    //        temp.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = quests[i].name;

    //        //대체 문자
    //        string replaceText = quests[i].details;
    //        replaceText = replaceText.Replace("'", ",");
    //        replaceText = replaceText.Replace("\\n", "\n");
    //        temp.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = replaceText;
    //    }
    //}

    public void Active(int index)
    {
        quests = DatabaseManager.instance.GetQuest(index);
        if (quests.Length!=0)
        {

            if (index == 0) // 처음 대화 끝에 0으로 활성화 시킴
            {
                GameObject.FindWithTag("Won").gameObject.SetActive(false);
                completeQuest = true;
            }

            for (int i = 0; i < quests[0].name.Length; i++)
            {
                questBox[i].SetActive(true);
                //questBox[i].transform.GetChild(0).GetComponent<TMP_Text>().text = quests[index].name[i];
                questBox[i].transform.GetChild(0).GetComponent<TMP_Text>().text = quests[0].name[i];


                //대체 문자
                //string replaceText = quests[index].details[i];
                string replaceText = quests[0].details[i];
                replaceText = replaceText.Replace("'", ",");
                replaceText = replaceText.Replace("\\n", "\n");

                //questDetailList[i].transform.GetChild(0).GetComponent<TMP_Text>().text = quests[index].name[i];
                questDetailList[i].transform.GetChild(0).GetComponent<TMP_Text>().text = quests[0].name[i];
                questDetailList[i].transform.GetChild(1).GetComponent<TMP_Text>().text = replaceText;



            }
        }

        if (completeQuest)
        {
            storage.saveData.nowIndex++;
            completeQuest = false;
        }
    }


}
