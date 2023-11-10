using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardParser : MonoBehaviour
{
    public Reward_[] Parse(string _CVSFileName)
    {
        List<Reward_> rewardList = new List<Reward_>();
        TextAsset csvData = Resources.Load<TextAsset>(_CVSFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 2; i < data.Length;)
        {
           
            string[] row = data[i].Split(new char[] { ',' });

            Reward_ reward = new Reward_();

            reward.rewardIndex = row[0];
            //reward.rewardContexts = row[5];
            //reward.rewardItem = row[6];
            Debug.Log(row[5]);
            List<string> contextList = new List<string>();
            List<string> itemList = new List<string>();
            do
            {
                contextList.Add(row[5]);
                itemList.Add(row[6]);
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
            reward.rewardContexts = contextList.ToArray();
            reward.rewardItem = itemList.ToArray();
            rewardList.Add(reward);

        }

        return rewardList.ToArray();
    }
}
