using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
   public void GoSea()
    {
        GameManager.instance.ChangeScene("Sea");
    }

    public void GoBeach()
    {
        GameManager.instance.ChangeScene("Beach");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Room");
    }
}
