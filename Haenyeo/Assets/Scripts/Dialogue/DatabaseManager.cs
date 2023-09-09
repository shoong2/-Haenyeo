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
                questDic.Add(j + 1, quests[j]);
            }
        }
    }

    public Dialogue[] GetDialogue(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for(int i=0; i<=_EndNum -_StartNum; i++)
        {
            dialogueList.Add(dialogueDic[_StartNum + i]);
        }

        return dialogueList.ToArray();
    }

    public Quest[] GetQuest(int questCount)
    {
        List<Quest> questList = new List<Quest>();

        for(int i=0; i<questCount; i++)
        {
            questList.Add(questDic[storage.saveData.questAllCount +1 + i]);
        }

        //for(int i=0; i<= _EndNum - _StartNum; i++)
        //{
        //    questList.Add(questDic[_StartNum + i]);
        //}

        return questList.ToArray();
    }

}
