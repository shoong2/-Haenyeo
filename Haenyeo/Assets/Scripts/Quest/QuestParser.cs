using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestParser : MonoBehaviour
{
    public Quest[] Parse(string _CVSFileName)
    {
        List<Quest> questList = new List<Quest>();
        TextAsset csvData = Resources.Load<TextAsset>(_CVSFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        for(int i=3; i<data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Quest quest = new Quest();

            quest.name = row[5];
            quest.details = row[6];

            Debug.Log(row[5]);
            //List<string> detailList = new List<string>();

            //detailList.Add(row[6]);
            Debug.Log(row[6]);

            do
            {
             
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            }
            while (row[5].ToString() == "");

            //while (row[5].ToString() == "")
            //{
            //    if (++i < data.Length)
            //    {
            //        row = data[i].Split(new char[] { ',' });
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            //quest.details = detailList.ToArray();

            questList.Add(quest);
        }

        return questList.ToArray();
    }
}
