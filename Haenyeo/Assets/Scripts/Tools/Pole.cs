using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : Tool
{

    public float poleRayLine =5f;
    public override void Init()
    {
        data.rayLine = 3f;
        data.toolName = "Pole";
    }

    public override void SetRaycast(Vector3 dir, Player player)
    {
        Debug.DrawRay(player.rigid.position, dir * data.rayLine, Color.red);
        poleRaycast = Physics2D.CircleCast(player.transform.position, poleRayLine, Vector3.zero, 1, LayerMask.GetMask("Piscse"));
       // Debug.Log(fish);
        raycast = Physics2D.Raycast(player.transform.position, dir, data.rayLine, LayerMask.GetMask("Piscse"));
        activeAttack = true;
    }

    public override void AttachFish(Player player, RaycastHit2D ray)
    {
        base.AttachFish(player,ray);
        poleRaycast.collider.transform.GetComponent<Fish>().ShowCanvas(player.transform.position.x);
    }

    public override void GetRaycast(GameObject toolGuideGroup, Player player, GameObject[] toolGuide)
    {
        base.GetRaycast(toolGuideGroup, player, toolGuide);
        if(poleRaycast)
        {
            fish = poleRaycast.collider.GetComponent<Fish>();
            GuideSetting(poleRaycast.collider.tag, toolGuideGroup, toolGuide);
            AttachFish(player, poleRaycast);
        }
        else
        {
            if (fish != null)
            {
                fish.EnableCanvas();
                activeAttack = false;
            }
            toolGuideGroup.SetActive(false);
            
        }

    }

    public override void ToolAttack()
    {
        fish.SetHP();
        StartCoroutine(AttractCoroutine());
        Debug.Log("Pole");
    }

    private void OnDrawGizmos()
    {
        Player player = FindAnyObjectByType<Player>();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.transform.position, poleRayLine);
    }

    IEnumerator AttractCoroutine()
    {
        while (!raycast)
        {
            // 캐릭터의 위치
            Vector3 characterPosition = FindAnyObjectByType<Player>().transform.position;
            Vector3 direction = characterPosition - fish.transform.position;

            fish.transform.position += direction.normalized * 10 * Time.deltaTime;

            // 일정한 시간 간격을 두고 반복
            yield return new WaitForSeconds(0.01f);
        }
    }
}
