using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : Fish
{
    public GameObject mark;
    public float markXPos = 1f;
    Rigidbody2D rigid;
    public float reverseMoveSpeed = 1f;

    SpriteRenderer render;
    protected override void Start()
    {
        base.Start();

        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }
    public override void ShowCanvas(float playerXPos)
    {
        mark.SetActive(true);
        base.ShowCanvas(playerXPos);
        
        if (left)
        {
            mark.transform.localPosition = Vector2.right;
        }
        else
            mark.transform.localPosition = -Vector2.right;


           StartCoroutine(ReverseMove(left));
       // Invoke("ReverseMove", 0.5f);
        
    }

    public override void EnableCanvas()
    {
        base.EnableCanvas();
        mark.SetActive(false);
    }

    IEnumerator ReverseMove(bool left)
    {
        if (left)
        {
            rigid.AddForce(-Vector2.right, ForceMode2D.Force);
            render.flipX = true;
        }
        else
        {
            rigid.AddForce(Vector2.right, ForceMode2D.Force);
            render.flipX = false;
        }
    
        yield return new WaitForSeconds(reverseMoveSpeed);
        rigid.velocity = Vector2.zero;
    }

}
