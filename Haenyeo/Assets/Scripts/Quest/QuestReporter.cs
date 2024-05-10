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

    private void Start()
    {
        ReportIfPassCondition(SceneManager.GetActiveScene().name);
    }

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
       // Debug.Log(target.name);
        QuestSystem.Instance.ReceiveReport(category, target, successCount);
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
