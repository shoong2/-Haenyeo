using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UnderSea : Player
{
    Vector3 targetPosition;
    Vector3 tewakTargetPosition;
    Animator playerAnim;
    public float rayLine = 3f;
    
    

    [SerializeField]
    GameObject toolGuide;

    [SerializeField]
    GameObject[] tools;
    protected override void Start()
    {
        y = 0;
        restrictY = 2f;
        base.Start();
        playerAnim = GetComponent<Animator>();
        targetPosition = new Vector2(transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y - objectHeight*2f);
        transform.position = new Vector2(transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + objectHeight*2f);
        tewakTargetPosition = new Vector2(transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + objectHeight * 3f);
        StartCoroutine(StartUnderSea());
    }

    //protected override void FixedUpdate()
    //{
    //    base.FixedUpdate();
    //    //Vector3 dir = render.flipX ? Vector3.right : Vector3.left;
    //    //Debug.DrawRay(rigid.position, dir * rayLine, Color.red);
    //    //RaycastHit2D raycast = Physics2D.Raycast(rigid.position, dir * rayLine);
    //}

    private void Update()
    {
        Vector3 dir = render.flipX ? Vector3.right : Vector3.left;
        Debug.DrawRay(rigid.position, dir * rayLine, Color.red);
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, dir,rayLine, LayerMask.GetMask("Item"));

        //if(raycast.collider.tag =="Knife")
        //{
        //    Debug.Log("?");
        //    toolGuide.SetActive(true);
        //    toolGuide.transform.position = tools[0].transform.position;
        //}
        if (raycast.collider.tag =="Knife")
        {

            Debug.Log(raycast.collider.name);
        }
    }


    IEnumerator StartUnderSea()
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        playerAnim.SetTrigger("Swim");
        restrictY = 1f;

    }

    


}
