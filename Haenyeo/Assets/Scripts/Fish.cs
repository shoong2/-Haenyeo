using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Fish : MonoBehaviour
{
    public UnityEvent onFishDead;

    public GameObject canvas;

    public Image hp;
    public float maxHP=3;
    public float curHp;

    public float fadeSpeed = 0.1f;

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

    SpriteRenderer renderer;

    void Start()
    {
        camera = Camera.main;
        curHp = maxHP;
        objectWidth = transform.localScale.x;
        objectHeight = transform.localScale.y;
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
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
        if (curHp > 0)
        {
            canvas.SetActive(true);
            if (gameObject.transform.position.x > playerXPos)
            {
                canvas.transform.localPosition = new Vector2(hpPos, 0);
            }
            else
                canvas.transform.localPosition = new Vector2(-hpPos, 0);
        }
    }

    public void EnableCanvas()
    {
        canvas.SetActive(false);
    }    

    public void SetHP(int damage=1)
    {
        curHp-= damage;
        if(curHp ==0)
        {
            Debug.Log("Die");
        }
        else
            hp.fillAmount = curHp / maxHP;
    }

    public void Die()
    {
        onFishDead.Invoke();

        EnableCanvas();
        StartCoroutine(FadeOut());
       // Destroy(gameObject);
    }

    //void FadeOut()
    //{
    //    while(renderer.color.a >0)
    //    {
    //        renderer.color= new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - Time.deltaTime * fadeSpeed);
    //    }
        
    //}

    IEnumerator FadeOut()
    {
        //for(int i=10; i>=0; i--)
        //{
        //    float f = i / 10f;
        //    Color c = renderer.material.co
        //}
        while (renderer.color.a > 0)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a -  fadeSpeed);
            yield return new WaitForSeconds(fadeSpeed);
        }
        Destroy(gameObject);
    }
}
