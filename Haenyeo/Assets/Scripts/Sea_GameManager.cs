using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Sea_GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Animator playerAnim;

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

    public GameObject[] ship;

    public GameObject yeong;
    public GameObject seo;
    public GameObject yoon;


    private void Awake()
    {
        playerAnim = player.GetComponent<Animator>();
        if(GameManager.instance == null)
        {
            player.SetActive(true);
            shipAnim.SetBool("Start", true);
        }
        else if (GameManager.instance.previousSceneName == "Room")
        {
            Debug.Log("active");
            StartCoroutine(StartSea());
        }
        else
        {
            player.SetActive(true);
            shipAnim.SetBool("Start",true);
        }
    }
    void Start()
    {
        //if(GameManager.instance.index >=5)
        //{
        //    Debug.Log("tt");
        //    yeong.SetActive(false);
        //    seo.SetActive(true);
        //    yoon.SetActive(true);
        //}
        if (GameManager.instance != null)
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
        }


    }

    void ChangeDay(int day)
    {
        skyImg.sprite = sky[day];
        seaImg.sprite = sea[day];

        for(int i=0; i< ship.Length; i++)
        {
            ship[i].SetActive(false);
            if(i==day)
            {
                ship[i].SetActive(true);
            }
        }
    }

    IEnumerator StartSea()
    {
        divePlayer.SetActive(true);
        playerAnim.SetTrigger("Jump");
        //while(player.transform.position.y>-2f)
        //{
        //    player.transform.position -= new Vector3(0, 0.1f,0);
        //    yield return new WaitForSeconds(0.05f);
        //}
        //yield return new WaitUntil(() => !playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"));
        yield return new WaitForSeconds(waitTime);
        player.SetActive(true);
        Debug.Log("test");
        shipAnim.SetBool("Start", true);

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
