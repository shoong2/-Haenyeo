using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_UnderSea : Player
{
    Vector3 targetPosition;
    Vector3 tewakTargetPosition;
    Animator playerAnim;
    public float rayLine = 3f;
    public GameObject seaHP;
    //[SerializeField]
    //GameObject hp;
    //[SerializeField]
    //Image hpSlider;
    //public float maxHp = 10f;
    //float currentHp;
    //public float hpX =2f;

    [SerializeField]
    GameObject toolGuide;

    [SerializeField]
    GameObject[] tools;
    protected override void Start()
    {
        Debug.Log("start?");
        y = 0;
        restrictY = 2f;
        base.Start();
        playerAnim = GetComponent<Animator>();
        targetPosition = new Vector2(transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y - objectHeight*2f);
        transform.position = new Vector2(transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + objectHeight*2f);
        tewakTargetPosition = new Vector2(transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + objectHeight * 3f);
        StartCoroutine(StartUnderSea());
    }

    

    private void Update()
    {
        Vector3 dir = render.flipX ? Vector3.right : Vector3.left;
        Debug.DrawRay(rigid.position, dir * rayLine, Color.red);
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, dir, rayLine, LayerMask.GetMask("Item"));

        
        if (raycast)
        {
            if (raycast.collider.tag == "Knife")
            {
                GuideSetting(0);
                GameObject seahp = Instantiate(seaHP, raycast.collider.transform.position,
                    Quaternion.identity, GameObject.Find("UnderSea").transform);
                
            }
            else if (raycast.collider.tag == "Hoe")
            {
                GuideSetting(1);
            }
            else if (raycast.collider.tag == "Pole")
            {
                //toolGuide.SetActive(true);
                //toolGuide.transform.position = tools[2].transform.position;
                //toolGuide.transform.parent = tools[2].transform;
                GuideSetting(2);
            }
        }
        else
            toolGuide.SetActive(false);


        

    }

    void GuideSetting(int index)
    {
        toolGuide.SetActive(true);
        toolGuide.transform.position = tools[index].transform.position;
        toolGuide.transform.parent = tools[index].transform;
    }


    IEnumerator StartUnderSea()
    {
        //currentHp = maxHp;
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2f * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        //playerAnim.SetTrigger("Swim");
        playerAnim.SetBool("Move", false);
        restrictY = 1f;

    }

    
    public void Hoe()
    {
        playerAnim.SetTrigger("Hoe");
    }
    
    public void Knife()
    {
        playerAnim.SetTrigger("Knife");
    }

    public void Pole()
    {
        playerAnim.SetTrigger("Pole");
        //playerAnim.SetBool("test", true);
    }

}
