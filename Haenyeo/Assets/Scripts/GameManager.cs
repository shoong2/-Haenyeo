using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    GameObject joystick;
    
    [SerializeField]
    GameObject canvas_;

    [SerializeField]
    GameObject underSeaUI;
    [SerializeField]
    GameObject hp;
    [SerializeField]
    Image hpSlider;
    public float maxHp = 10f;
    float currentHp;

    [SerializeField]
    GameObject player_UnderSea;
    SpriteRenderer render;

    public float hpX = 2f;
    Canvas canvasComp;

    VariableJoystick joy;

    Camera mainCamera;

    bool underSea = false;

    public string previousSceneName;

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
        if (underSea) //hp 따라다니기
        {
            //Vector3 hpPos = mainCamera.WorldToScreenPoint(player_UnderSea.transform.position);
            hp.transform.position = player_UnderSea.transform.position + new Vector3(render.flipX ? -2f : 1 * hpX, 0, 0);
            hpSlider.fillAmount = currentHp / maxHp;
            currentHp -= Time.deltaTime;

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
        if (scene.name != "Room")
        {
            joystick.SetActive(true);
            joy.JoystickReset();
            mainCamera = Camera.main;
            canvasComp.worldCamera = mainCamera;
            underSeaUI.SetActive(false);
            player_UnderSea.SetActive(false);
            underSea = false;
            if(scene.name != "Sea")
            {
                currentHp = maxHp; //현재 hp 최대 hp로 초기화
                underSea = true;
                underSeaUI.SetActive(true);
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
        if(!underSea)
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
}
