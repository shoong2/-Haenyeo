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

    public float hpPos = 1.5f;
    void Start()
    {
        curHp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowCanvas(float playerXPos)
    {
        canvas.SetActive(true);
        if(gameObject.transform.position.x > playerXPos)
        {
            canvas.transform.localPosition = new Vector2(hpPos,0);
        }
        else
            canvas.transform.localPosition = new Vector2(-hpPos, 0);
    }

    public void EnableCanvas()
    {
        canvas.SetActive(false);
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
