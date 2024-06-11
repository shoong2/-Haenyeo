using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tteesstt : MonoBehaviour
{
    public GameObject longBar;
    public GameObject shortBar;
    RectTransform longRect;
    RectTransform shortRect;

    public float longBarSpeed;
    public float shortBarSpeed;


    private void Start()
    {
        longRect = longBar.GetComponent<RectTransform>();
        shortRect = shortBar.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (longBar.transform.localPosition.x + (longRect.rect.width) / 2 > transform.GetComponent<RectTransform>().rect.width / 2)
        {
            longBarSpeed = -longBarSpeed;

        }
        else if (longBar.transform.localPosition.x - (longRect.rect.width) / 2 < -transform.GetComponent<RectTransform>().rect.width / 2)
        {
            longBarSpeed = Mathf.Abs(longBarSpeed);

        }

        if (shortBar.transform.localPosition.x + (shortRect.rect.width) / 2 > transform.GetComponent<RectTransform>().rect.width / 2)
        {
            shortBarSpeed = -shortBarSpeed;

        }
        else if (shortBar.transform.localPosition.x - (shortRect.rect.width) / 2 < -transform.GetComponent<RectTransform>().rect.width / 2)
        {
            shortBarSpeed = Mathf.Abs(shortBarSpeed);

        }

        longBar.transform.Translate(longBarSpeed * Time.deltaTime, 0, 0);
        shortBar.transform.Translate(shortBarSpeed * Time.deltaTime, 0, 0);
    }

    public void click()
    {
        if (longBar.transform.position.x + longRect.rect.width / 2 > shortBar.transform.position.x + shortRect.rect.width / 2 &&
           longBar.transform.position.x - longRect.rect.width / 2 < shortBar.transform.position.x - shortRect.rect.width / 2)
        {
            Debug.Log("In");
        }
    }
}
