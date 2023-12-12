using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnderSeaGameManager : MonoBehaviour
{
    public GameObject player;

    public TMP_Text distaceText;
    float distance;

    SpriteRenderer render;
    Camera camera;

    [Header("HP")]
    public GameObject seaHP;
    public Image hpSlider;
    public float hpX = 2f; //hp x위치
    public float maxHp = 10f;
    public float currentHp;
    bool activeHp =true;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
        currentHp = maxHp;
        camera = Camera.main;
        render = player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {


        distance = -(player.transform.position.y - 6f);
        distaceText.text = distance.ToString("F1")+"M";

        seaHP.transform.position = player.transform.position + new Vector3(render.flipX ? -hpX : hpX, 0 , 0); // hp 따라다니기
        //currentHp -= Time.deltaTime;
        hpSlider.fillAmount = currentHp / maxHp;

        if (currentHp < 0 && activeHp)
        {
            activeHp = false;
            StartCoroutine(TewakMove());
        }
        else
            currentHp -= Time.deltaTime;
    }

    public void Tewak()
    {
        StartCoroutine(TewakMove());
    }
    IEnumerator TewakMove() // 죽었을 때와 같이 쓰기
    {
        //SoundManager.instance.PlaySE("Tewak");
        if (!activeHp)
        {
            player.GetComponent<Animator>().SetTrigger("Die");
        }
        else
            player.GetComponent<Animator>().SetTrigger("Tewak");

        activeHp = false;
        seaHP.SetActive(false);
        yield return new WaitForSeconds(1.3f);
        //Vector3 tewakTargetPosition = new Vector2(player_UnderSea.transform.position.x, mainCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + 3f);
        Vector3 tewakTargetPosition = new Vector2(player.transform.position.x, camera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y + 3f);
        //while (player_UnderSea.transform.position != tewakTargetPosition)
        while (player.transform.position != tewakTargetPosition)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, tewakTargetPosition, distance * Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene("Sea");
        //GameManager.instance.ChangeScene("Sea");
    }

    public void TimeControl()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    
}
