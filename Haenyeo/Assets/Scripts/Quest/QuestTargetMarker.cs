using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuestTargetMarker : MonoBehaviour
{
    //TryAddTargetQuest 함수에서 퀘스트에 있는 모든 타겟을 조회할 때 비활성화 돼있는 재원도 조회가 돼서
    //분리시켜서 할 방법 생각하기


    [SerializeField]
    TaskTarget target;
    [SerializeField]
    MarkerMaterialData[] markerMaterialDatas;

    Dictionary<Quest, Task> targetTasksByQuest = new Dictionary<Quest, Task>();
    //Transform cameraTransform;
    //Renderer renderer;

    int currentRunningTargetTaskCount;

    private void Awake()
    {
        //cameraTransform = Camera.main.transform;
       // renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameObject.SetActive(false);

        QuestSystem.Instance.onQuestRegistered += TryAddTargetQuest;
        foreach (var quest in QuestSystem.Instance.ActiveQuests)
            TryAddTargetQuest(quest);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ClearEvent();
    }


    private void OnDestroy()
    {
        ClearEvent();
    }

    void ClearEvent()
    {
        QuestSystem.Instance.onQuestRegistered -= TryAddTargetQuest;
        foreach ((Quest quest, Task task) in targetTasksByQuest)
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
        Debug.Log("발생");
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
        Debug.Log(currentState);
        if (currentState == TaskState.Running)
        {
            //renderer.material = markerMaterialDatas.First(x => x.category == task.Category).markerMatrial;
            currentRunningTargetTaskCount++;
        }
        else
            currentRunningTargetTaskCount--;


        gameObject.SetActive(currentRunningTargetTaskCount != 0); //true

        if (currentRunningTargetTaskCount == 0)
        {
            Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    struct MarkerMaterialData
    {
        public Category category;
        public Material markerMatrial;
    }
}
