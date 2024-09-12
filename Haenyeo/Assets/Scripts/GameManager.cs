
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    GameObject joystick;
    
    [SerializeField]
    GameObject canvas_;

    [SerializeField]
    GameObject underSeaUI;

    [SerializeField] GameObject questBox;
    [SerializeField] TMP_Text questText;
    bool isquest = false;

    Canvas canvasComp;


    public Camera mainCamera;

    bool underSea = false;

    public GameObject phone;

    public string previousSceneName;

    public float time = 0;
    public float maxTime = 30;

    public TMP_Text timer;
    public float questTime =60;
    bool isQuestTime = false;
    public enum State { Idle, Afternoon, Night };
    public State state = State.Idle;

    //����Ŭ�� ����
    int ClickCount = 0;

    [Header("�����")]
    public SaveNLoad storage;
    public int index;
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        mainCamera = Camera.main;
        canvasComp = canvas_.GetComponent<Canvas>();
        SceneManager.sceneLoaded += OnSceneLoaded;
       
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
       
        if(isquest)
        {
            questTime -= Time.deltaTime;
            timer.text = ((int)questTime).ToString() + "s";
        }


        if (time <= maxTime / 3)
        {
            state = State.Idle;
        }
        else if (time <= maxTime / 3 * 2)
        {
            state = State.Afternoon;
        }
        else
        {
            state = State.Night;
        }

        if (underSea) 
        {
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
           // SpriteRenderer render = player.GetComponent<SpriteRenderer>();
            if (time<maxTime)
                time += Time.deltaTime;

        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            Application.Quit();
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        //index = storage.saveData.nowIndex;
        Debug.Log(scene.name);
        mainCamera = Camera.main;
        canvasComp.worldCamera = mainCamera;
        if (scene.name != "Room" && scene.name != "Beach")
        {
            underSeaUI.SetActive(false);

            underSea = false;
            timer.gameObject.SetActive(false);

            if (scene.name == "UnderSea")
            {
                UnderSeaGameManager underGM = FindAnyObjectByType<UnderSeaGameManager>();
                underSea = true;
                underGM.storage = this.storage;
                //if(index ==1)
                //{
                //    underGM.startQuestIndex_1 = true;
                //}
            }

        }
        else
        {
            joystick.SetActive(false);  
        }

    }

    public void ChangeScene(string sceneName)
    {
        SoundManager.instance.PlaySE("button");
        previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void CheckPhone()
    {
        SoundManager.instance.PlaySE("button");
        if (phone.activeSelf)
        {
            phone.SetActive(false);
        }
        else
            phone.SetActive(true);
    }

    public void ResetData()
    {
        storage.saveData.nowIndex = 0;
        storage.saveData.completeQuest = 0;
        storage.saveData.questAllCount = 0;
        storage.saveData.isQuest = false;
        storage.SaveData();
        SceneManager.LoadScene("Room");
    }

    void DoubleClick()
    {
        ClickCount = 0;
    }

    public void TimeSet()
    {
        Time.timeScale = 1f;
    }
}
