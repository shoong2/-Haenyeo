using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestParser : MonoBehaviour
{
    public Quest_[] Parse(string _CVSFileName)
    {
        List<Quest_> questList = new List<Quest_>();
        TextAsset csvData = Resources.Load<TextAsset>(_CVSFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        for(int i=3; i<data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Quest_ quest = new Quest_();

            quest.questIndex = row[0];
           // quest.name = row[5];
          //  quest.details = row[6];


            List<string> detailList = new List<string>();
            List<string> nameList = new List<string>();
            //detailList.Add(row[6]);


            do
            {
                if (nameList.Count == 0)
                {
                    nameList.Add(row[5]);
                    detailList.Add(row[6]);
                    //Debug.Log("hi");
                }
                else if (row[5] == "")
                {
                    //nameList.Add(nameList[nameList.Count - 1]);
                    //detailList.Add(detailList[detailList.Count - 1]);
                    //Debug.Log(2);
                
               
                }
                else
                {
                    nameList.Add(row[5]);
                    detailList.Add(row[6]);
                    Debug.Log(3);
                }

                //nameList.Add(row[5]);
                //contextList.Add(row[6]);
                //Debug.Log(row[6]);

                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            }
            while (row[0].ToString() == "");

            quest.details = detailList.ToArray();
            quest.name = nameList.ToArray();

            questList.Add(quest);
        }

        return questList.ToArray();
    }
}
