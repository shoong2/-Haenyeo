using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy; //조이스틱

    public float speed = 1f; //움직임 속도
    float objectWidth;
    float objectHeight;

    Rigidbody2D rigid; 

    Vector2 moveVec; 

    SpriteRenderer render; //좌우반전 스프라이트 렌더러

    Camera mainCamera;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        objectWidth = transform.localScale.x;
        objectHeight = transform.localScale.y;
    }

    private void FixedUpdate()
    {
        float x = joy.Horizontal;
        float y = joy.Vertical;

        //좌우반전
        if (x >= 0)
            render.flipX = true;
        else
            render.flipX = false;

        moveVec = rigid.position + new Vector2(x, y) * speed * Time.deltaTime;
       
        //카메라 밖으로 나가지 않도록 제한
        float leftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + objectWidth / 2;
        float rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - objectWidth / 2;
        float topEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.68f, 0)).y - objectHeight / 2;
        float bottomEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + objectHeight / 2;

        moveVec = new(Mathf.Clamp(moveVec.x, leftEdge, rightEdge), 
            Mathf.Clamp(moveVec.y, bottomEdge, topEdge));

        rigid.MovePosition(moveVec);
    }


}
