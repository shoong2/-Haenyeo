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

    [SerializeField] TMP_Text meter;
    float meterNum;
    [SerializeField] GameObject questBox;
    [SerializeField] TMP_Text questText;
    bool isquest = false;

    [SerializeField]
    GameObject hp;
    [SerializeField]
    Image hpSlider;
    public float maxHp = 10f;
    public float currentHp;

    [SerializeField]
    GameObject player_UnderSea;
    SpriteRenderer render;

    public float hpX = 2f;
    Canvas canvasComp;

    VariableJoystick joy;

    public Camera mainCamera;

    bool underSea = false;

    public GameObject phone;

    public string previousSceneName;

    public float time = 0;
    public float maxTime = 30;

    public enum State { Idle, Afternoon, Night };
    public State state = State.Idle;

    [Header("저장소")]
    [SerializeField] SaveNLoad storage;
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
        joy = joystick.GetComponent<VariableJoystick>();
        render = player_UnderSea.GetComponent<SpriteRenderer>();
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
       
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

        if (underSea) //hp 따라다니기
        {
            time += Time.deltaTime; //바다에 들어가면 시간 카운트
            //Vector3 hpPos = mainCamera.WorldToScreenPoint(player_UnderSea.transform.position);
            hp.transform.position = player_UnderSea.transform.position + new Vector3(render.flipX ? -2f : 1 * hpX, 0, 0);
            hpSlider.fillAmount = currentHp / maxHp;
            currentHp -= Time.deltaTime;

            meterNum = (-(player_UnderSea.transform.position.y - 6f) / 7f);
            meter.text = meterNum.ToString("F1") +"M";

            if(!isquest && meterNum>3f)
            {
                isquest = false;
                questText.text = "3M 까지 버티기";
                questBox.SetActive(true);
                storage.saveData.isQuest = true;
                storage.SaveData();
               
            }

            if(currentHp <=0)
            {
                underSea = false;
                StartCoroutine(TewakMove());
            }
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        mainCamera = Camera.main;
        canvasComp.worldCamera = mainCamera;
        if (scene.name != "Room")
        {
            joystick.SetActive(true);
            joy.JoystickReset();
            //mainCamera = Camera.main;
            //canvasComp.worldCamera = mainCamera;
            underSeaUI.SetActive(false);
            player_UnderSea.SetActive(false);
            meter.gameObject.SetActive(false);
            underSea = false;
            if(scene.name != "Sea")
            {
                currentHp = maxHp; //현재 hp 최대 hp로 초기화
                underSea = true;
                underSeaUI.SetActive(true);
                meter.gameObject.SetActive(true);
                player_UnderSea.SetActive(true);
            }

        }
        else
        {
            joystick.SetActive(false);  
        }
    }

    public void Tewak()
    {
        StartCoroutine(TewakMove());
    }
    IEnumerator TewakMove() // 죽었을 때와 같이 쓰기
    {
        underSeaUI.SetActive(false);
        meter.gameObject.SetActive(false);
        if (!underSea)
        {
            player_UnderSea.GetComponent<Animator>().SetTrigger("Die");
        }
        else
            player_UnderSea.GetComponent<Animator>().SetTrigger("Tewak");
        yield return new WaitForSeconds(1.3f);
        Vector3 tewakTargetPosition = new Vector2(player_UnderSea.transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + 3f);
        while (player_UnderSea.transform.position != tewakTargetPosition)
        {
            player_UnderSea.transform.position = Vector3.MoveTowards(player_UnderSea.transform.position, tewakTargetPosition, 3.5f * Time.deltaTime);
            yield return null;
        }
        //SceneManager.LoadScene("Sea");
        ChangeScene("Sea");
    }

    public void ChangeScene(string sceneName)
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void CheckPhone()
    {
        if (phone.activeSelf)
        {
            phone.SetActive(false);
        }
        else
            phone.SetActive(true);
    }
}
