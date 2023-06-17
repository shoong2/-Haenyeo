using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fish : MonoBehaviour
{
    public GameObject canvas;

    public Image hp;
    public float maxHP=3;
    public float curHp;

    void Start()
    {
        curHp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag =="Player")
    //    {
    //        canvas.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if(collision.tag=="Player")
    //    {
    //        canvas.SetActive(false);
    //    }
    //}

    public void ShowCanvas()
    {
        canvas.SetActive(true);
    }

    public void SetHP()
    {
        curHp--;
        if(curHp ==0)
        {
            Debug.Log("Die");
        }
        else
            hp.fillAmount = curHp / maxHP;
    }
}
