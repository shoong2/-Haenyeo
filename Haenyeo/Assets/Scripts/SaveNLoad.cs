using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int nowIndex = 0;
    public bool isQuest = false;
}

public class SaveNLoad : MonoBehaviour
{
    public SaveData saveData = new SaveData();

    string SAVE_DATA_DIRECTORY;
    string SAVE_FILENAME = "/SaveFile.txt";


    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        SAVE_DATA_DIRECTORY = Application.persistentDataPath + "/Saves/";

        if(!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
            SaveData();
        }
        else
        {
            LoadData();
        }

    }

    public void SaveData()
    {

        string json = JsonUtility.ToJson(saveData,true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);

            saveData = JsonUtility.FromJson<SaveData>(loadJson);
        }
    }
}
