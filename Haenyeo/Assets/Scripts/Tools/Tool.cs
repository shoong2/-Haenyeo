using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Data
{
    public float damage;
    public float rayLine;
    public string toolName;
}

public abstract class Tool : MonoBehaviour
{
    public Data data;

    protected RaycastHit2D raycast;
    protected RaycastHit2D poleRaycast;
    //RaycastHit2D currentRay;
    protected Vector3 dir;


    //public GameObject[] toolGuide;
    public bool activeAttack = false;
    public Fish fish;

    //레이 설정
    public abstract void SetRaycast(Vector3 dir, Player player);
    public abstract void Init();
    public abstract void ToolAttack();



    //공격
    public void Attack(Animator playerAnim, UnderSeaGameManager gm, Inventory inven, bool runningQuest)
    {
        playerAnim.SetTrigger(data.toolName);
        if (activeAttack)
        {
            ToolAttack();
            if (fish.curHp <= 0)
            {
                gm.CatchWindow(fish.transform.GetComponent<ItemPickUp>().item);
                if (fish != null)
                    inven.AcquireItem(fish.transform.GetComponent<ItemPickUp>().item, runningQuest);

                fish.Die();
            }
        }
    }

    //물고기에 붙었을 때
    public virtual void AttachFish(Player player,  RaycastHit2D ray)
    {
        ray.collider.transform.GetComponent<Fish>().ShowCanvas(player.transform.position.x);
    }

    //레이 작동
    public virtual void GetRaycast(GameObject toolGuideGroup, Player player, GameObject[] toolGuide)
    {
        if (raycast)
        {
            fish = raycast.collider.GetComponent<Fish>();
            GuideSetting(raycast.collider.tag,toolGuideGroup, toolGuide);
            AttachFish(player, raycast);
        }
        else
        {
            if (fish != null)
            {
                fish.EnableCanvas();
                if(!poleRaycast)
                    activeAttack = false;

            }
            toolGuideGroup.SetActive(false);
            
            
        }
    }


    public void GuideSetting(string coliderItemName, GameObject toolGuideGroup, GameObject[] toolGuide)
    {
        toolGuideGroup.SetActive(true);
        for (int i = 0; i < toolGuide.Length; i++)
        {
            toolGuide[i].SetActive(toolGuide[i].name == coliderItemName);
        }

    }
}
