using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeafoodManagerNew : MonoBehaviour
{
    public static SeafoodManagerNew Instance { get; private set; }

    public Dictionary<string, int> seafoodCountDict = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 초기 해산물 수량 설정
            seafoodCountDict.Add("seafood1", 1);
            seafoodCountDict.Add("seafood2", 2);
            seafoodCountDict.Add("seafood3", 3);
            seafoodCountDict.Add("recipe_numA", 0);
            seafoodCountDict.Add("recipe_numB", 0);
            seafoodCountDict.Add("recipe_numC", 0);
            seafoodCountDict.Add("recipe_numD", 0); // 새로운 레시피 추가

            Debug.Log("SeafoodManagerNew Awake: Initialized seafood counts.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetSeafoodCount(string seafoodName)
    {
        if (seafoodCountDict.ContainsKey(seafoodName))
        {
            return seafoodCountDict[seafoodName];
        }
        else
        {
            Debug.LogWarning("Seafood not found in the dictionary: " + seafoodName);
            return 0;
        }
    }

     void UpdateSeafoodCount(string seafoodName, int count)
    {
        if (seafoodCountDict.ContainsKey(seafoodName))
        {
            seafoodCountDict[seafoodName] = count;
            Debug.Log("Updated seafood count for " + seafoodName + ": " + count);
        }
        else
        {
            Debug.LogWarning("Seafood not found in the dictionary: " + seafoodName);
        }
    }

}






