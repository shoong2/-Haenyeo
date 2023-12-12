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

    //[SerializeField] TMP_Text meter;
    //float meterNum;
    [SerializeField] GameObject questBox;
    [SerializeField] TMP_Text questText;
    bool isquest = false;

    //[SerializeField]
    //GameObject hp;
    //[SerializeField]
    //Image hpSlider;
    //public float maxHp = 10f;
    //public float currentHp;

    //[SerializeField]
    //GameObject player_UnderSea;

    //public float hpX = 2f;
    Canvas canvasComp;

    //VariableJoystick joy;

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
    [SerializeField] SaveNLoad storage;
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

    private void Start()
    {
        //joy = joystick.GetComponent<VariableJoystick>();
        //render = player_UnderSea.GetComponent<SpriteRenderer>();
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
        //if(time>=maxTime)
        //{
        //    time = 0;
        //}

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

        if (underSea) //hp ����ٴϱ�
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            SpriteRenderer render = player.GetComponent<SpriteRenderer>();
            if (time<maxTime)
                time += Time.deltaTime; //�ٴٿ� ���� �ð� ī��Ʈ
            //Vector3 hpPos = mainCamera.WorldToScreenPoint(player_UnderSea.transform.position);
            //hp.transform.position = player.transform.position + new Vector3(render.flipX ? -2f : 1 * hpX, 0, 0);
            //hpSlider.fillAmount = currentHp / maxHp;
            //currentHp -= Time.deltaTime;

            //meterNum = (-(player_UnderSea.transform.position.y - 6f) / 7f);
            //meter.text = meterNum.ToString("F1") +"M";

            //if(!isquest && meterNum>3f)
            //{
            //    isquest = true;
            //    questText.text = "3M ���� ��Ƽ��";
            //    questBox.SetActive(true);
            //    storage.saveData.isQuest = true;
            //    storage.SaveData();
               
            //}

            //if(currentHp <=0)
            //{
            //    underSea = false;
            //    StartCoroutine(TewakMove());
            //}
        }

        //���� ����
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
        
        index = storage.saveData.nowIndex;
        Debug.Log(scene.name);
        mainCamera = Camera.main;
        canvasComp.worldCamera = mainCamera;
        if (scene.name != "Room" && scene.name != "Beach")
        {
           // joystick.SetActive(true);
           // joy.JoystickReset();
            //mainCamera = Camera.main;
            //canvasComp.worldCamera = mainCamera;

            //�ٴ�ӿ��� ������ ���
            underSeaUI.SetActive(false);
           // player_UnderSea.SetActive(false);
            //meter.gameObject.SetActive(false);
            underSea = false;
            timer.gameObject.SetActive(false);

            if (scene.name == "UnderSea")
            {
                //currentHp = maxHp; //���� hp �ִ� hp�� �ʱ�ȭ
                underSea = true;
                //underSeaUI.SetActive(true);
                //meter.gameObject.SetActive(true);
                //player_UnderSea.SetActive(true);

        //        if (storage.saveData.nowIndex == 6)
        //        {
        //            timer.gameObject.SetActive(true);
        //            isquest = true;
        //}
            }

        }
        else
        {
            joystick.SetActive(false);  
        }
    }

    //public void Tewak()
    //{
    //    StartCoroutine(TewakMove());
    //}
    //IEnumerator TewakMove() // �׾��� ���� ���� ����
    //{
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    underSeaUI.SetActive(false);
    //    float nowDistace = UnderSeaGameManager.distance;
    //   // meter.gameObject.SetActive(false);
    //    if (!underSea)
    //    {
    //        player.GetComponent<Animator>().SetTrigger("Die");
    //    }
    //    else
    //        player.GetComponent<Animator>().SetTrigger("Tewak");
    //    yield return new WaitForSeconds(1.3f);
    //    //Vector3 tewakTargetPosition = new Vector2(player_UnderSea.transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + 3f);
    //    Vector3 tewakTargetPosition = new Vector2(player.transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + 3f);
    //    //while (player_UnderSea.transform.position != tewakTargetPosition)
    //    while (player.transform.position != tewakTargetPosition)
    //    {
    //        player.transform.position = Vector3.MoveTowards(player.transform.position, tewakTargetPosition, nowDistace * Time.deltaTime);
    //        yield return null;
    //    }
    //    //SceneManager.LoadScene("Sea");
    //    ChangeScene("Sea");
    //}

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
}
