using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [Tooltip("퀘스트 이름")]
    public string name;

    [Tooltip("퀘스트 내용")]
    public string[] details;

    public bool isQuest = false;
}

[System.Serializable]
public class QuestEvent
{
    public string name;

    public Vector2 line;
    public Quest[] quests;
}
