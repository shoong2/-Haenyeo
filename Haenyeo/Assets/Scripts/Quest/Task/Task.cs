using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TaskState
{
    Inactive,
    Running,
    Complete
}

[CreateAssetMenu(menuName ="Quest/Task/Task", fileName ="Task_")]
public class Task : ScriptableObject
{
    #region Events
    public delegate void StateChangedHandler(Task task, TaskState currentState, TaskState prevState);
    public delegate void SuccessChangedHandler(Task task, int currentSuccess, int prevSuccess);
    #endregion

    [SerializeField]
    Category category;

    [Header("Text")]
    [SerializeField]
    string codeName;
    [SerializeField]
    string description;

    [Header("Action")]
    [SerializeField]
    TaskAction action;

    [Header("Setting")]
    [SerializeField]
    int needSuccessToComplete;
    [SerializeField]
    InitialSuccessValue initialSuccessValue;
    [SerializeField]
    bool CanReceiveReportsDuringCompletion;

    [Header("Target")]
    [SerializeField]
    TaskTarget[] targets;

    TaskState state;
    int currentSuccess;

    public event StateChangedHandler onStateChanged;
    public event SuccessChangedHandler onSuccessChanged;
    public int CurrentSuccess
    {
        get => currentSuccess;
        set
        {
            int prevSuccess = currentSuccess;
            currentSuccess = Mathf.Clamp(value, 0, needSuccessToComplete);
            if (currentSuccess != prevSuccess)
            {
                State = currentSuccess == needSuccessToComplete ? TaskState.Complete : TaskState.Running;
                onSuccessChanged?.Invoke(this, currentSuccess, prevSuccess);
            }
        }
    }

    public int NeedSuccessToComplete => needSuccessToComplete;
    public string CodeName => codeName;
    public string Description => description;
    public Category Category => category;

    public TaskState State
    {
        get => state;
        set
        {
            var prevState = state;
            state = value;
            onStateChanged?.Invoke(this, state, prevState);
        }
    }

    public bool IsComplete => State == TaskState.Complete;
    public Quest Owner { get; set; }

    public void Setup(Quest owner)
    {
        Owner = owner;
    }

    public void Start()
    {
        State = TaskState.Running;
        if (initialSuccessValue)
            CurrentSuccess = initialSuccessValue.GetValue(this);
    }

    public void End()
    {
        onStateChanged = null;
        onSuccessChanged = null;
    }

    public void complete()
    {
        CurrentSuccess = needSuccessToComplete;
    }

    public void ReceieveReport(int successCount)
    {
        CurrentSuccess = action.Run(this, CurrentSuccess, successCount); 
    }

    public bool IsTarget(string category, object target)
        => Category == category&&
        targets.Any(x => x.IsEqual(target)) &&
        (!IsComplete || (IsComplete && CanReceiveReportsDuringCompletion));

    public bool ContainsTarget(object target) => targets.Any(x => x.IsEqual(target));
}
