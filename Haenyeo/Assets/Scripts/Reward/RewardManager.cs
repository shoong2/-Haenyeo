

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RewardManager : MonoBehaviour
{
    //[Header("리워드 이름")]
    //[SerializeField] TMP_Text rewardNameText;

    //[Header("리워드 알림")]
    //[SerializeField] TMP_Text rewardContest;

    [SerializeField] GameObject notiList;
    [SerializeField] GameObject rewardBox;

    Reward_[] rewards;
    public void GetReward(int index)
    {
        rewards = DatabaseManager.instance.GetReward(index);
        Debug.Log("here");
        Debug.Log(rewards[index].rewardContexts.Length);
        for(int i=0; i<rewards[index].rewardContexts.Length; i++)
        {
            GameObject rBox = Instantiate(rewardBox);
            rBox.transform.SetParent(notiList.transform);
            rBox.transform.localScale = new Vector3(1, 1, 1);
            rBox.transform.localPosition = new Vector3(0, 0, 0);

            rBox.transform.GetChild(0).GetComponent<TMP_Text>().text = rewards[index].rewardItem[i];
            rBox.transform.GetChild(1).GetComponent<TMP_Text>().text = rewards[index].rewardContexts[i];
        }
    }
}
