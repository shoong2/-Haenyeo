using System;
using System.Collections.Generic;
using UnityEngine;


public enum QuestGoalType_
{
    Kill,
    Capture
}


//[CreateAssetMenu]
//public class CaptureQuestTask_ : QuestTask_
//{
//    public string captureItem;
//    public int Amount;
//}

[Serializable]
public class QuestGoal_
{
    public QuestGoalType_ questGoalType;
    public QuestTask_ questTask;
}

[CreateAssetMenu]
public class Quest__ : ScriptableObject
{
    public string title;
    public string description;
    // .etc

    public List<QuestGoal_> questGoals = new List<QuestGoal_>();
}


