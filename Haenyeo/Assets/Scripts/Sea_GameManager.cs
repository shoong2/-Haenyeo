using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Sea_GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject divePlayer;

    [SerializeField]
    Animator shipAnim;


    [SerializeField]
    float waitTime=0.5f;

    public Image skyImg;
    public Image seaImg;
    //Sky
    public Sprite[] sky;
    //Sea
    public Sprite[] sea;
    void Start()
    {
        if(GameManager.instance.time <=GameManager.instance.maxTime/3)
        {

        }
        else if(GameManager.instance.time <= GameManager.instance.maxTime / 3*2)
        {

        }
        else
        {

        }
        if(GameManager.instance.previousSceneName =="Room")
            StartCoroutine(StartSea());
        else
        {
            player.SetActive(true);
            shipAnim.Play("ShipAnim", -1);
        }

    }

    IEnumerator StartSea()
    {
        divePlayer.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        player.SetActive(true);
        shipAnim.Play("ShipAnim", -1);

    }

    public void GoHome()
    {
        SceneManager.LoadScene("Room");
    }


    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            skyImg.sprite = sky[2];
        }    
    }

}
