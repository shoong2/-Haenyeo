using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TaskState
{
    Inactive,
    Running,
    complete
}

[CreateAssetMenu(menuName ="Quest/Task/Task", fileName ="Task_")]
public class Task : ScriptableObject
{
    #region Events
    public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState taskState);
    public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevSuccess);
    #endregion

    [SerializeField]
    Category category;

    [Header("Text")]
    [SerializeField]
    string codeName;
    [SerializeField]
    string description; //어떤 테스크인지 알려줄

    [Header("Action")]
    [SerializeField]
    TaskAction action;

    [Header("Target")]
    [SerializeField]
    TaskTarget[] targets;

    [Header("Setting")]
    [SerializeField]
    InitialSuccessValue initialSuccessvalue;
    [SerializeField]
    int needSuccessToComplete;
    [SerializeField]
    bool canReceiveReportsDuringCompletion;

    TaskState state;
    int currentSuccess;

    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;

    public int CurrentSuccess
    {
        get => currentSuccess;
        set
        {
            int prevSucceess = currentSuccess;
            currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
            if(currentSuccess != prevSucceess)
            {
                State = currentSuccess == needSuccessToComplete ? TaskState.complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, currentSuccess, prevSucceess);
            }
        }
    }
    public Category Category => category;

    //외부에서 보일 수 있게 만든 것
    public string CodeName => codeName;
    public string Description => description;
    public int NeedSuccessTocomplete => needSuccessToComplete;
    public TaskState State
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            onStateChanged?.Invoke(this, state, prevState); //?의미는 ? 뒤에 함수가 null이면 null 반환 아니면 뒤에 함수 실행

        }
    }

    public bool IsComplete => State == TaskState.complete;
    public Quest_ Owner { get; set; }

    public void Setup(Quest_ owner)
    {
        Owner = owner;
    }

    public void Start()
    {
        State = TaskState.Running;
        if (initialSuccessvalue)
            CurrentSuccess = initialSuccessvalue.GetValue(this);
    }    

    public void End()
    {
        onStateChanged = null;
        onSuccessChanged = null;
    }
    public void ReceiveReport(int successCount)
    {
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount);
    }

    public void Complete()
    {
        CurrentSuccess = needSuccessToComplete;
    }

    public bool IsTarget(string category, object target)
        => Category == category &&
        targets.Any(x => x.IsEqual(target)) &&
        (!IsComplete || (IsComplete && canReceiveReportsDuringCompletion));

}
