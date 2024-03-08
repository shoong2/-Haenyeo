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
    protected Vector3 dir;


    //public GameObject[] toolGuide;
    protected bool activeAttack = false;
    protected Fish fish;

    public abstract void SetRaycast(Vector3 dir, Player player);
    public abstract void Init();

    public void Attack(Animator playerAnim, UnderSeaGameManager gm, Inventory inven)
    {
        playerAnim.SetTrigger(data.toolName);
        if (activeAttack)
        {
            fish.SetHP();
            if (fish.curHp <= 0)
            {
                gm.CatchWindow(fish.transform.GetComponent<ItemPickUp>().item);
                if (fish != null)
                    inven.AcquireItem(fish.transform.GetComponent<ItemPickUp>().item);

                fish.Die();
            }
        }
    }

    public void AttachFish(Player player)
    {
        raycast.collider.transform.GetComponent<Fish>().ShowCanvas(player.transform.position.x);
    }

    public void GetRaycast(GameObject toolGuideGroup, Player player, GameObject[] toolGuide)
    {
        if (raycast)
        {
            fish = raycast.collider.GetComponent<Fish>();
            GuideSetting(raycast.collider.tag,toolGuideGroup, toolGuide);
            AttachFish(player);
        }
        else
        {
            if (fish != null)
            {
                fish.EnableCanvas();

            }
            toolGuideGroup.SetActive(false);
            activeAttack = false;
        }
    }

    void GuideSetting(string coliderItemName, GameObject toolGuideGroup, GameObject[] toolGuide)
    {
        toolGuideGroup.SetActive(true);
        for (int i = 0; i < toolGuide.Length; i++)
        {
            toolGuide[i].SetActive(toolGuide[i].name == coliderItemName);
        }

    }
}
