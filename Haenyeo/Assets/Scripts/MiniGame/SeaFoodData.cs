using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeafoodData
{
    public string name;
    public int count;

    public SeafoodData(string name, int count)
    {
        this.name = name;
        this.count = count;
    }
}

public class SeafoodManager : MonoBehaviour
{
    public static SeafoodManager Instance { get; private set; }

    public List<SeafoodData> seafoodList = new List<SeafoodData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 예시 데이터 추가
            seafoodList.Add(new SeafoodData("seafood1", 0));
            seafoodList.Add(new SeafoodData("seafood2", 0));
            seafoodList.Add(new SeafoodData("seafood3", 0));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetSeafoodCount(string name)
    {
        SeafoodData data = seafoodList.Find(seafood => seafood.name == name);
        return data != null ? data.count : 0;
    }

    public void UpdateSeafoodCount(string name, int count)
    {
        SeafoodData data = seafoodList.Find(seafood => seafood.name == name);
        if (data != null)
        {
            data.count = count;
        }
        else
        {
            seafoodList.Add(new SeafoodData(name, count));
        }
    }
}

