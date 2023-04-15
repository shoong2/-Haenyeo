using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject joystick;
    
    [SerializeField]
    GameObject canvas_;

    Canvas canvasComp;

    VariableJoystick joy;
    private void Awake()
    {
        canvasComp = canvas_.GetComponent<Canvas>();
        //canvas_ = GetComponent<Canvas>();
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
            Camera mainCamera = Camera.main;
            //canvas_.worldCamera = mainCamera;
            canvasComp.worldCamera = mainCamera;
        }
        else
        {
            joystick.SetActive(false);  
        }
    }


}
