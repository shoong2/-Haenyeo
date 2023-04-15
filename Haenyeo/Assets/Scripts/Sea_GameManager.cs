using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea_GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    Animator shipAnim;

    [SerializeField]
    float waitTime=0.5f;
    void Start()
    {
        StartCoroutine(StartSea());
    }

    IEnumerator StartSea()
    {
        yield return new WaitForSeconds(waitTime);
        player.SetActive(true);
        shipAnim.Play("ShipAnim", -1);

    }
}
