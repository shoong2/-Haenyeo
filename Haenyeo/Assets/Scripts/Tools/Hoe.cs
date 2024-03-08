using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : Tool
{
    public override void Init()
    {
        data.rayLine = 3f;
        data.toolName = "Hoe";
    }

    public override void SetRaycast(Vector3 dir, Player player)
    {
        Debug.DrawRay(player.rigid.position, dir * data.rayLine, Color.red);
        raycast = Physics2D.Raycast(player.transform.position, dir, data.rayLine, LayerMask.GetMask("Item") + LayerMask.GetMask("Piscse"));
        activeAttack = true;
    }
}
