using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActiveQuest : ScriptableObject
{
    public string title;
    public string description;

    public List<QuestGoal> questGoals = new List<QuestGoal>();
}

public enum QuestType
{
    GetItem,
    Mission,
    Other
}

[CreateAssetMenu]
public class GetItemQuestTask : QuestTask
{
    public string Target;
    public int Amount;
}

[CreateAssetMenu]
public class MissonQuestTask : QuestTask
{
    public string captureItem;
    public int Amount;
}




