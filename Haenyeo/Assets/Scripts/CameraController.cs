using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 0.125f; // 카메라 이동 스무딩을 위한 속도
    public Transform target; // 카메라가 따라갈 오브젝트의 Transform 컴포넌트


    private void Start()
    {
        //target = GameObject.Find("UnderSea_Player").transform;
    }
    private void LateUpdate()
    {
        
        Vector3 desiredPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        if(target.transform.position.y<=0 || transform.position.y <=0)
            transform.position = smoothedPosition;
        

    }
}
