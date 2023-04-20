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

    GameObject player;

    Canvas canvasComp;

    VariableJoystick joy;

    Camera mainCamera;

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
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        if (scene.name != "Room")
        {
            joystick.SetActive(true);
            joy.JoystickReset();
            mainCamera = Camera.main;
            //canvas_.worldCamera = mainCamera;
            canvasComp.worldCamera = mainCamera;
            underSeaUI.SetActive(false);
            if(scene.name != "Sea")
            {
                underSeaUI.SetActive(true);
                player = GameObject.FindGameObjectWithTag("Player");
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
    IEnumerator TewakMove()
    {
        underSeaUI.SetActive(false);
        player.GetComponent<Animator>().SetTrigger("Tewak");
        yield return new WaitForSeconds(1.3f);
        Vector3 tewakTargetPosition = new Vector2(player.transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + 3f);
        while (player.transform.position != tewakTargetPosition)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, tewakTargetPosition, 3.5f * Time.deltaTime);
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
