using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : Tool
{
    public override void Init()
    {
        data.rayLine = 7f;
        data.toolName = "Pole";
    }

    public override void SetRaycast(Vector3 dir, Player player)
    {
        Debug.DrawRay(player.rigid.position, dir * data.rayLine, Color.red);
        raycast = Physics2D.CircleCast(player.transform.position, data.rayLine, Vector3.zero, 1, LayerMask.GetMask("Piscse"));
        raycast = Physics2D.Raycast(player.transform.position, dir, data.rayLine, LayerMask.GetMask("Item") + LayerMask.GetMask("Piscse"));
        activeAttack = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, data.rayLine);
    }
}
