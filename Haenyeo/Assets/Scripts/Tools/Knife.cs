using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Tool
{

    public float knifeRaySize;
    public override void Init()
    {
        data.rayLine = knifeRaySize;
        data.toolName = "Knife";
    }

    public override void SetRaycast(Vector3 dir, Player player)
    {
        Debug.DrawRay(player.transform.position, dir * data.rayLine, Color.red);
        raycast = Physics2D.Raycast(player.transform.position, dir, data.rayLine, LayerMask.GetMask("Item") + LayerMask.GetMask("Piscse"));
        activeAttack = true;
    }

    public override void ToolAttack()
    {
        fish.SetHP();
    }
}
