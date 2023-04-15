using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UnderSea : Player
{
    Vector3 targetPosition;
    protected override void Start()
    {
        y = 0;
        restrictY = 2f;
        base.Start();
        //targetPosition = new Vector3(transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y - 2f, 0);
        //transform.position = new Vector2(transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y - objectHeight / 2);
        //StartCoroutine(StartUnderSea());
    }

    //IEnumerator StartUnderSea()
    //{
    //    restrict = false;
    //    while(transform.position!= targetPosition)
    //    {
    //        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f * Time.deltaTime);
    //    }
    //    yield return new WaitForSeconds(2f);
    //    restrict = true;
    //}
}
