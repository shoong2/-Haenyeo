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

    [Header("움직임")]
    Rigidbody2D rigid;
    Vector2 moveVec;
    float leftEdge;
    float rightEdge;
    Camera camera;
    public float speed = 3f;

    SpriteRenderer renderer;

    public bool left = false;

    protected virtual void Start()
    {
        camera = Camera.main;
        curHp = maxHP;
        objectWidth = transform.localScale.x;
        objectHeight = transform.localScale.y;
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        leftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + objectWidth / 2;
        rightEdge = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - objectWidth / 2;

        moveVec = new(Mathf.Clamp(moveVec.x, leftEdge, rightEdge), transform.position.y);
       // rigid.MovePosition(moveVec);
    }


    virtual public void ShowCanvas(float playerXPos)
    {
        if (curHp > 0)
        {
            canvas.SetActive(true);
            if (gameObject.transform.position.x > playerXPos) // 더 오른쪽에 있는 경우
            {
                left = false;
                canvas.transform.localPosition = new Vector2(hpPos, 0);
            }
            else
            {
                left = true;
                canvas.transform.localPosition = new Vector2(-hpPos, 0);
            }
        }
    }

    virtual public void EnableCanvas()
    {
        canvas.SetActive(false);
    }    

    public void SetHP(int damage=1)
    {
        curHp-= damage;
        if(curHp ==0)
        {
            onFishDead.Invoke();
            Debug.Log("Die");
            EnableCanvas();
            StartCoroutine(FadeOut());
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



    IEnumerator FadeOut()
    {
        while (renderer.color.a > 0)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a -  fadeSpeed);
            yield return new WaitForSeconds(fadeSpeed);
        }
        Destroy(gameObject);
    }
}
