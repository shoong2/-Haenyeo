using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
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
}
