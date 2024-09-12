using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class QuestTrackerView : MonoBehaviour
{
    [SerializeField]
    QuestTracker questTrackerPrefab;

    [SerializeField]
    CategoryColor[] categoryColors;

  

    private void Start()
    {
        QuestSystem.Instance.onQuestRegistered += CreateQuestTracker;
        foreach (var quest in QuestSystem.Instance.ActiveQuests)
        {
            CreateQuestTracker(quest);
        }

    }

    private void OnDestroy()
    {
        if (QuestSystem.Instance)
            QuestSystem.Instance.onQuestRegistered -= CreateQuestTracker;
    }

    void CreateQuestTracker(Quest quest)
    {
        Debug.Log("create ui");
        var categoryColor = categoryColors.FirstOrDefault(x => x.category == quest.Category);
        var color = categoryColor.category == null ? Color.white : Color.black;//categoryColor.color;

        Debug.Log(categoryColor.category);
        //foreach(var task in quest.CurrentTaskGroup.Tasks)
        //    if(task.CodeName!="dialogue")
        Instantiate(questTrackerPrefab, transform).Setup(quest, color);
    }

    [System.Serializable]
    struct CategoryColor
    {
        public Category category;
        public Color color;
    }
}

