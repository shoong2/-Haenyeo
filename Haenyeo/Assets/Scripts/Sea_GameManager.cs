using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Sea_GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject divePlayer;

    [SerializeField]
    Animator shipAnim;


    [SerializeField]
    float waitTime=0.5f;
    void Start()
    {
        if(GameManager.instance.previousSceneName =="Room")
            StartCoroutine(StartSea());
        else
        {
            player.SetActive(true);
            shipAnim.Play("ShipAnim", -1);
        }

    }

    IEnumerator StartSea()
    {
        divePlayer.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        player.SetActive(true);
        shipAnim.Play("ShipAnim", -1);

    }

    public void GoHome()
    {
        SceneManager.LoadScene("Room");
    }

    void OnMouseDown()
    {
        Debug.Log("click");
        // 마우스 클릭 위치의 Raycast 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        // Raycast 수행
        if (Physics.Raycast(ray, out hitInfo))
        {
            // 클릭한 오브젝트의 태그 확인
            if (hitInfo.collider.gameObject.CompareTag("Ship"))
            {
                Debug.Log("ship");
                SceneManager.LoadScene("Room");
            }
        }
    }

}
