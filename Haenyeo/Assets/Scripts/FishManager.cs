using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{

    public TTest[] test;
    public GameObject[] b;
    public GameObject a;
    private void Start()
    {
        //test = GameObject.FindObjectOfType<TTest>().gameObject;

        test = FindObjectsOfType<TTest>();
        b = new GameObject[test.Length];
        for (int i = 0; i < test.Length; i++)
        {
            
            b[i] = Instantiate(a, test[i].gameObject.transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        }
    }

    private void Update()
    {
        for (int i = 0; i < test.Length; i++)
        {
            b[i].transform.position = test[i].transform.position + new Vector3(2, 0, 0);
        }
    }

    public void Check(GameObject w)
    {
        for (int i = 0; i < test.Length; i++)
        {
            if(test[i].transform.name == w.transform.name)
            {
                b[i].SetActive(true);
            }
        }
    }

    public void Active(GameObject w)
    {
        for (int i = 0; i < test.Length; i++)
        {
            if (test[i].transform.name == w.transform.name)
            {
                b[i].SetActive(false);
            }
        }
    }
}
