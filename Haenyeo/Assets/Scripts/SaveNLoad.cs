using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int nowIndex = 0;
    public bool isQuest = false;
    public int questAllCount = 0; //�����Ѱ� ���� �������� ����Ʈ ī��Ʈ
    public int completeQuest = 0; // �Ϸ��� ����Ʈ

    public List<int> invenArrayNumber = new List<int>();
    public List<string> invenItemName = new List<string>();
    public List<int> invenItemNumber = new List<int>();
}

public class SaveNLoad : MonoBehaviour
{
    //public static SaveNLoad instance = null;

    public SaveData saveData = new SaveData();

    string SAVE_DATA_DIRECTORY;
    string SAVE_FILENAME = "/SaveFile.txt";

    Inventory theInven;



    void Start()
    {
       // theInven = GetComponent<Inventory>();
        Debug.Log(Application.persistentDataPath);
        SAVE_DATA_DIRECTORY = Application.persistentDataPath + "/Saves/";

        if(!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
            //SaveData();
        }
        else
        {
            Debug.Log("load");
            //LoadData();
        }

    }

    public void SaveData()
    {
        theInven = FindObjectOfType<Inventory>();
        //theInven = GetComponent<Inventory>();
        Slot[] slots = theInven.GetSlots();
        Debug.Log(slots.Length);
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }

        string json = JsonUtility.ToJson(saveData,true);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);

            saveData = JsonUtility.FromJson<SaveData>(loadJson);
            theInven = FindObjectOfType<Inventory>();

            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                theInven.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);
            }
        }
    }


    //������ �ӽ� ����
    //private void OnApplicationQuit()
    //{
    //    SaveData();
    //}
}
