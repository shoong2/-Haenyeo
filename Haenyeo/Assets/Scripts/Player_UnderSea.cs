using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Player_UnderSea : Player
{
    Vector3 targetPosition;
    Vector3 tewakTargetPosition;
    Animator playerAnim;
    public float rayLine = 3f;

    RaycastHit2D raycast;
    Fish fish;

    [Header("Tool")]
    [SerializeField]
    GameObject[] toolGuide;
    public GameObject toolGuideGroup;
    public ToolManager toolManager;

    [SerializeField]
    GameObject[] tools;

    [SerializeField] Inventory inven;

    bool click = false;

    bool activeAttack = false;

    bool startSea = false; // 바다에 들어가자마자 y좌표 이상으로 올라와서 씬 이동 방지

    public GameObject getBox;
    public TMP_Text getText;
    public Image getSprite;
    protected override void Start()
    {
        Debug.Log("start?");
        y = 0;
        restrictY = 2f;
        base.Start();
        //currentHp = maxHp;
        playerAnim = GetComponent<Animator>();
        //targetPosition = new Vector2(transform.position.x, GameManager.instance.mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y);//.y - objectHeight*2f);
        //transform.position = new Vector2(transform.position.x, GameManager.instance.mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + objectHeight*2f);
        //tewakTargetPosition = new Vector2(transform.position.x, GameManager.instance.mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + objectHeight * 3f);
        targetPosition = new Vector2(transform.position.x, camera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y);//.y - objectHeight*2f);
        transform.position = new Vector2(transform.position.x, camera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + objectHeight * 2f);
        tewakTargetPosition = new Vector2(transform.position.x, camera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + objectHeight * 3f);
        StartCoroutine(StartUnderSea());
    }

    

    private void Update()
    {
        //seaHP.transform.position = gameObject.transform.position + new Vector3(render.flipX ? -2f : 1 * hpX, 0, 0); // hp 따라다니기
        //currentHp -= Time.deltaTime;
        //hpSlider.fillAmount = currentHp / maxHp;


        Vector3 dir = render.flipX ? Vector3.right : Vector3.left;
        if (toolManager.activeToolName == "Pole")
        {
            rayLine = 8f;
        }
        else
            rayLine = 3f;

        Debug.DrawRay(rigid.position, dir * rayLine, Color.red);
        raycast = Physics2D.Raycast(transform.position, dir, rayLine, LayerMask.GetMask("Item"));


        if (raycast)
        {
            fish = raycast.collider.GetComponent<Fish>();
            toolGuideGroup.SetActive(true);
            GuideSetting(raycast.collider.tag);
            if (toolManager.activeToolName == "Knife")
            {
                raycast.collider.transform.GetComponent<Fish>().ShowCanvas(gameObject.transform.position.x);
                activeAttack = true; 
            }
            else if (toolManager.activeToolName == raycast.collider.tag)
            {
                raycast.collider.transform.GetComponent<Fish>().ShowCanvas(gameObject.transform.position.x);
                activeAttack = true;
            }
            else
            {
                raycast.collider.transform.GetComponent<Fish>().EnableCanvas();
                activeAttack = false;
            }
            //if (!test)
            //{
            //    GameObject seahp = Instantiate(seaHP, raycast.collider.transform.position,
            //      Quaternion.identity, GameObject.Find("UnderSea").transform);
            //    seahp.transform.position += new Vector3(2, 0, 0);
            //    test = true;
            //}
            //toolGuideGroup.SetActive(true);
            //if (raycast.collider.tag == "Knife")
            //{

            //    GuideSetting(knife, raycast.collider.gameObject);
            //    raycast.collider.transform.GetComponent<Fish>().ShowCanvas();
            //    //GameObject seahp = Instantiate(seaHP, raycast.collider.transform.position,
            //    //    Quaternion.identity, GameObject.Find("UnderSea").transform);

            //}
            //else if (raycast.collider.tag == "Hoe")
            //{
            //    GuideSetting(hoe, raycast.collider.gameObject);
            //    raycast.collider.transform.GetComponent<Fish>().ShowCanvas();
            //}
            //else if (raycast.collider.tag == "Pole")
            //{
            //    //toolGuide.SetActive(true);
            //    //toolGuide.transform.position = tools[2].transform.position;
            //    //toolGuide.transform.parent = tools[2].transform;
            //    GuideSetting(pole, raycast.collider.gameObject);
            //    raycast.collider.transform.GetComponent<Fish>().ShowCanvas();
            //}
            //Debug.Log("det");
        }
        else
        {
            if(fish!=null)
            {
                fish.EnableCanvas();
            }
            toolGuideGroup.SetActive(false);
            activeAttack = false;
           // raycast.collider.transform.GetComponent<Fish>().canvas.SetActive(false);
        }

        if (transform.position.y >= 4f || transform.position.y <= -35)
        {
            //GameManager.instance.mainCamera.transform.GetComponent<CameraController>().enabled = false;
            camera.transform.GetComponent<CameraController>().enabled = false;
            if (startSea)
                SceneManager.LoadScene("Sea");
        }
        else
            camera.transform.GetComponent<CameraController>().enabled = true;
        //GameManager.instance.mainCamera.transform.GetComponent<CameraController>().enabled = true;


    }

    void GuideSetting(string coliderItemName)//, GameObject fish)
    {
        for (int i = 0; i < toolGuide.Length; i++)
        {
            toolGuide[i].SetActive(toolGuide[i].name == coliderItemName);
        }

        //if (click == true)
        //{
        //    if (fish.transform.GetComponent<Fish>().curHp > 0)
        //    {
        //        fish.transform.GetComponent<Fish>().SetHP();
        //        if(fish.transform.GetComponent<Fish>().curHp ==0)
        //        {
        //            Destroy(fish.gameObject);
        //            getText.text = "<b>[" + fish.transform.GetComponent<ItemPickUp>().item.itemName + "]</b> 을\n채집했습니다!";
        //            getSprite.sprite = fish.transform.GetComponent<ItemPickUp>().item.itemImage;
        //            inven.AcquireItem(fish.transform.GetComponent<ItemPickUp>().item);
        //            getBox.SetActive(true);
        //        }
        //        click = false;
        //    }
            //else
            //{
            //    getText.text = "<b>[" + fish.transform.GetComponent<ItemPickUp>().item.itemName + "]</b> 을\n채집했습니다!";
            //    getSprite.sprite = fish.transform.GetComponent<ItemPickUp>().item.itemImage;
            //    inven.AcquireItem(fish.transform.GetComponent<ItemPickUp>().item);
            //    getBox.SetActive(true);
            //}
        //}
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
        startSea = true;
    }

    public void Attack()
    {
        playerAnim.SetTrigger(toolManager.activeToolName);
        Debug.Log(toolManager.activeToolName);
        SoundManager.instance.PlaySE(toolManager.activeToolName);
        if(activeAttack)
        {
            fish.SetHP();
            if(fish.curHp<=0)
            {
                inven.AcquireItem(fish.transform.GetComponent<ItemPickUp>().item);
                fish.Die();
            }
            

        }

    }

    public void HoeAttack()
    {
        playerAnim.SetTrigger("Hoe");
        if(activeAttack)
        {
            fish.SetHP();
        }
        click = true;
    }
    
    public void KnifeAttack()
    {
        playerAnim.SetTrigger("Knife");
        if (activeAttack)
        {
            fish.SetHP();
        }
        click = true;
    }

    public void PoleAttack()
    {
        playerAnim.SetTrigger("Pole");
        if (activeAttack)
        {
            fish.SetHP();
        }
        click = true;
        //playerAnim.SetBool("test", true);
    }

}
