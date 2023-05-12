using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
     VariableJoystick joy; //조이스틱

    public float speed = 1f; //움직임 속도
    float objectWidth;
    protected float restrictY = 0.68f;
    protected float objectHeight;

    protected float x;
    protected float y;
    protected bool restrict = true;

    protected Rigidbody2D rigid; 

    Vector2 moveVec; 

    protected SpriteRenderer render; //좌우반전 스프라이트 렌더러

    Animator playerAnim;
    protected Camera mainCamera;

    float leftEdge;
    float rightEdge;
    float topEdge;
    float bottomEdge;

   

    private void Awake()
    {
        joy = FindObjectOfType<VariableJoystick>();
        playerAnim = GetComponent<Animator>();
    }

    virtual protected void Start()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        objectWidth = transform.localScale.x;
        objectHeight = transform.localScale.y;
    }

    virtual protected void FixedUpdate()
    {
        x = joy.Horizontal;
        y = joy.Vertical;

        //좌우반전
        if (x > 0)
        {
            render.flipX = true;
            //playerAnim.SetTrigger("Move");
            playerAnim.SetBool("Move", true);
        }
        else if (x < 0)
        {
            render.flipX = false;
            //playerAnim.SetTrigger("Move");
            playerAnim.SetBool("Move", true);
        }
        else
            playerAnim.SetBool("Move", false);
        //playerAnim.SetTrigger("Idle");

        moveVec = rigid.position + new Vector2(x, y) * speed * Time.deltaTime;
       
        //카메라 밖으로 나가지 않도록 제한

        leftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + objectWidth / 2;
        rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - objectWidth / 2;
        topEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, restrictY, 0)).y - objectHeight / 2;
        bottomEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + objectHeight / 2;


        moveVec = new(Mathf.Clamp(moveVec.x, leftEdge, rightEdge), 
            Mathf.Clamp(moveVec.y, bottomEdge, topEdge));


        rigid.MovePosition(moveVec);

        if (SceneManager.GetActiveScene().name=="Sea" && moveVec.y <= bottomEdge+2f)
        {
            SceneManager.LoadScene("UnderSea");
        }
    }


}
