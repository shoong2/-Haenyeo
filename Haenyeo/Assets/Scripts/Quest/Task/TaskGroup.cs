using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TaskGroupState
{
    Inactive,
    Running,
    Complete
}

[System.Serializable]
public class TaskGroup 
{
    [SerializeField]
    Task[] tasks;

    public IReadOnlyList<Task> Tasks => tasks;
    public Quest Owner { get; set; }
    public bool IsAllTaskComplete => tasks.All(x => x.IsComplete);
    public bool IsComplete => State == TaskGroupState.Complete;
    public TaskGroupState State { get; set; }

    public TaskGroup(TaskGroup copytarget)
    {
        tasks = copytarget.Tasks.Select(x => Object.Instantiate(x)).ToArray();
    }
    public void Setup(Quest owner)
    {
        Owner = owner;
        foreach (var task in tasks)
            task.Setup(owner);
    }

    public void Start()
    {
        State = TaskGroupState.Running;
        foreach (var task in tasks)
            task.Start();
    }

    public void End()
    {
        State = TaskGroupState.Complete;
        foreach (var task in tasks)
            task.End();
    }

    public void RecieveReport(string catecory, object target, int successCount)
    {
        foreach(var task in tasks)
        {
            if (task.IsTarget(catecory, target))
                task.ReceieveReport(successCount);
        }
    }

    public void Complete()
    {
        if (IsComplete)
            return;
        State = TaskGroupState.Complete;

        foreach(var task in tasks)
        {
            if(!task.IsComplete)
                task.complete();
        }
    }

    public Task FindTaskByTarget(object target) => tasks.FirstOrDefault(x => x.ContainsTarget(target));

    public Task FindTaskByTarget(TaskTarget target) => FindTaskByTarget(target.Value);

    public bool ContainsTarget(object target) => tasks.Any(x => x.ContainsTarget(target));

    public bool ContainsTarget(TaskTarget target) => ContainsTarget(target.Value);
}
