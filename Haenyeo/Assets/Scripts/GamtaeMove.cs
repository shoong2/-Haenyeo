using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamtaeMove : MonoBehaviour
{
    public float circleR;
    public float deg;
    public float objSpeed;

    Vector2 center;
    private void Awake()
    {
        center = transform.position;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deg += objSpeed * Time.deltaTime;
        transform.position = center + new Vector2(Mathf.Cos(deg), Mathf.Sin(deg)) * circleR;
        //deg += Time.deltaTime * objSpeed;
        //if (deg < 360)
        //{
        //    var rad = Mathf.Deg2Rad * (deg);
        //    var x = circleR * Mathf.Sin(rad);
        //    var y = circleR * Mathf.Cos(rad);
        //    transform.position = new Vector3(x, y);
        //    //transform.rotation = Quaternion.Euler(0, 0, deg * -1); //가운데를 바라보게 각도 조절
        //}
        //else
        //{
        //    deg = 0;
        //}

    }
}
