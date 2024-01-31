

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    [SerializeField] GameObject notiList;
    [SerializeField] GameObject rewardBox;

    Reward_[] rewards;

    public Item[] rewardItems;

    public void GetReward(int index)
    {
        rewards = DatabaseManager.instance.GetReward(index);
        Debug.Log("here");
        //Debug.Log(rewards[index].rewardContexts.Length);
        for(int i=0; i<rewards[index].rewardContexts.Length; i++)
        {
            GameObject rBox = Instantiate(rewardBox);
            rBox.transform.SetParent(notiList.transform);
            rBox.transform.localScale = new Vector3(1, 1, 1);
            rBox.transform.localPosition = new Vector3(0, 0, 0);

            rBox.transform.GetChild(0).GetComponent<TMP_Text>().text = rewards[index].rewardItem[i];
            rBox.transform.GetChild(1).GetComponent<TMP_Text>().text = rewards[index].rewardContexts[i];

            for (int j = 0; j < rewardItems.Length; j++)
            {
                Debug.Log(rewards[index].rewardItem[i].ToString());
                if(rewards[index].rewardItem[i].ToString() == rewardItems[j].itemName)
                {
                    //Debug.Log(rewards[index].rewardItem[i].ToString());
                    //Debug.Log(rewardItems[j].itemName);
                    rBox.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = rewardItems[j].itemImage;
                }
            }
        }
    }
}
