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

    float objectWidth;
    float objectHeight;

    public float hpPos = 1.5f;

    [Header("¿òÁ÷ÀÓ")]
    Rigidbody2D rigid;
    Vector2 moveVec;
    float leftEdge;
    float rightEdge;
    Camera camera;
    public float speed = 3f;

    void Start()
    {
        camera = Camera.main;
        curHp = maxHP;
        objectWidth = transform.localScale.x;
        objectHeight = transform.localScale.y;
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        leftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + objectWidth / 2;
        rightEdge = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - objectWidth / 2;
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
