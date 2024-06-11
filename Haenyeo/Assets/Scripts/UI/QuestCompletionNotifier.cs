using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class QuestCompletionNotifier : MonoBehaviour
{
    [SerializeField]
    GameObject questNoti;

    [SerializeField]
    string titleDescription;
    [SerializeField]
    TextMeshProUGUI titleText;
    [SerializeField]
    TextMeshProUGUI rewardText;
    [SerializeField]
    float showTime = 3f;

    Queue<Quest> reservedQuests = new Queue<Quest>();
    StringBuilder stringBuilder = new StringBuilder();

    private void Start()
    {
        var questSystem = QuestSystem.Instance;
        questSystem.onQuestCompleted += Notify;
        questSystem.onAchievementCompleted += Notify;

        //gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        var questSystem = QuestSystem.Instance;
        if(questSystem != null)
        {
            questSystem.onQuestCompleted -= Notify;
            questSystem.onAchievementCompleted -= Notify;
        }
    }

    void Notify(Quest quest)
    {
        Debug.Log("notify");
        reservedQuests.Enqueue(quest);

        if(!questNoti.activeSelf)
        {
            questNoti.SetActive(true);
            StartCoroutine(ShowNotice());
        }
    }

    IEnumerator ShowNotice()
    {
        var waitSeconds = new WaitForSeconds(showTime);

        Quest quest;

        while(reservedQuests.TryDequeue(out quest))
        {
            //string updateDescription = titleDescription.Replace("%{dn}", quest.DisplayName);
           // titleText.text = $"<b>{updateDescription}</b>";
           titleText.text = titleDescription.Replace("%{dn}", quest.DisplayName);
            foreach (var reward in quest.Rewards)
            {
                stringBuilder.Append(reward.Description);
                stringBuilder.Append(" ");
                stringBuilder.Append(reward.Quantitiy);
                stringBuilder.Append(" ");
            }
            rewardText.text = stringBuilder.ToString();
            stringBuilder.Clear();

            yield return waitSeconds;
        }

        //gameObject.SetActive(false);
    }

}
