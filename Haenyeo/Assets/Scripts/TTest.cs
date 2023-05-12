using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTest : MonoBehaviour
{
    public FishManager fishmanager;
    void Start()
    {
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            fishmanager.Check(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            fishmanager.Active(gameObject);
        }
    }
}
