using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuestReporter : MonoBehaviour
{
    [SerializeField]
    Category category;
    [SerializeField]
    TaskTarget target;
    [SerializeField]
    int successCount;
    [SerializeField]
    string[] colliderTags;
    [SerializeField]
    string sceneName;

    //private void Start()
    //{
    //    foreach (var quest in QuestSystem.Instance.ActiveQuests) //모든 reporter들이 작동해서 오류
    //    {
    //        if (quest.ContainsTarget("GoBeach"))
    //        {
    //            ReportIfPassCondition(SceneManager.GetActiveScene().name);
    //        }
    //    }
            
    //}

    private void OnTriggerEnter(Collider other)
    {
        ReportIfPassCondition(other);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReportIfPassCondition(collision);
    }

    public void Report()
    {
        QuestSystem.Instance.ReceiveReport(category, target, successCount);
        Debug.Log(000000000000);
    }

    void ReportIfPassCondition(Component other)
    {
        if (colliderTags.Any(x => other.CompareTag(x)))
            Report();
    }

    void ReportIfPassCondition(string other)
    {
        if (sceneName == other)
            Report();
    }
}
