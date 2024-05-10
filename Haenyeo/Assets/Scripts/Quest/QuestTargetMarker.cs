using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestTargetMarker : MonoBehaviour
{
    [SerializeField]
    TaskTarget target;
    [SerializeField]
    MarkerMaterialData[] markerMaterialDatas;

    Dictionary<Quest, Task> targetTasksByQuest = new Dictionary<Quest, Task>();
    Transform cameraTransform;
    Renderer renderer;

    int currentRunningTargetTaskCount;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        QuestSystem.Instance.onQuestRegistered += TryAddTargetQuest;
        foreach (var quest in QuestSystem.Instance.ActiveQuests)
            TryAddTargetQuest(quest);

        gameObject.SetActive(false);
    }

    //private void Update()
    //{
    //    var rotation = Quaternion.LookRotation((cameraTransform.position - transform.position).normalized);
    //    transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y + 180f, 0f);
    //}

    private void OnDestroy()
    {
        QuestSystem.Instance.onQuestRegistered -= TryAddTargetQuest;
        foreach((Quest quest, Task task) in targetTasksByQuest)
        {
            quest.onNewTaskGroup -= UpdateTargetTask;
            quest.onCompleted -= RemoveTargetQuest;
            task.onStateChanged -= UpdateRunningTargetTaskCount;
        }
    }


    void TryAddTargetQuest(Quest quest)
    {
        if(target !=null && quest.ContainsTarget(target))
        {
            quest.onNewTaskGroup += UpdateTargetTask;
            quest.onCompleted += RemoveTargetQuest;

            UpdateTargetTask(quest, quest.CurrentTaskGroup);
        }
    }

    void UpdateTargetTask(Quest quest, TaskGroup currentTaskGroup, TaskGroup prevTaskGroup = null)
    {
        targetTasksByQuest.Remove(quest);

        var task = currentTaskGroup.FindTaskByTarget(target);
        if(task!=null)
        {
            targetTasksByQuest[quest] = task;
            task.onStateChanged += UpdateRunningTargetTaskCount;

            UpdateRunningTargetTaskCount(task, task.State);
        }
    }

    void RemoveTargetQuest(Quest quest) => targetTasksByQuest.Remove(quest);

    void UpdateRunningTargetTaskCount(Task task, TaskState currentState, TaskState prevState = TaskState.Inactive)
    {
        if (currentState == TaskState.Running)
        {
            renderer.material = markerMaterialDatas.First(x => x.category == task.Category).markerMatrial;
            currentRunningTargetTaskCount++;
        }
        else
            currentRunningTargetTaskCount--;

        gameObject.SetActive(currentRunningTargetTaskCount != 0);
    }

    [System.Serializable]
    struct MarkerMaterialData
    {
        public Category category;
        public Material markerMatrial;
    }
}
