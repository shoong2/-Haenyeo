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

    //enum State {Idle, Afternoon, Night}

    

    public Image skyImg;
    public Image seaImg;
    //Sky
    public Sprite[] sky;
    //Sea
    public Sprite[] sea;
    void Start()
    {

        if ((int)GameManager.instance.state == 0)
        {
            //skyImg.sprite = sky[(int)State.Idle];
            //seaImg.sprite = sea[(int)State.Idle];
            ChangeDay(0);
        }
        else if ((int)GameManager.instance.state == 1)
        {
            //skyImg.sprite = sky[(int)State.Afternoon];
            ChangeDay(1);
        }
        else
        {
            //skyImg.sprite = sky[(int)State.Night];
            ChangeDay(2);
        }

        if (GameManager.instance.previousSceneName =="Room")
            StartCoroutine(StartSea());
        else
        {
            player.SetActive(true);
            shipAnim.Play("ShipAnim", -1);
        }

    }

    void ChangeDay(int day)
    {
        skyImg.sprite = sky[day];
        seaImg.sprite = sea[day];
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
