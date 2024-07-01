using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Start_cook : MonoBehaviour
{
    public GameObject MiniGame_bar;
    public GameObject MiniGame_ver_bar;
    public TMP_Text TimeCount;
    public Button cookButton;
    public GameObject timeImage;
    public GameObject greatImage;
    public GameObject goodImage;
    public GameObject badImage;

    private const float MoveRange = 3.5f;
    public float barMoveSpeed = 0.05f;
    public float verBarMoveSpeed = 0.01f;

    private bool isMoving = false;
    private bool isCounting = false;

    private Info infoScript;
    private SeafoodManagerNew seafoodManager;

    void Start()
    {
        TimeCount = GameObject.Find("TimeCount").GetComponent<TMP_Text>();
        cookButton = GameObject.Find("MiniGame_CookButton").GetComponent<Button>();
        timeImage = GameObject.Find("Canvas3/Result_image/timeover");
        badImage = GameObject.Find("Canvas3/Result_image/bad");
        goodImage = GameObject.Find("Canvas3/Result_image/good");
        greatImage = GameObject.Find("Canvas3/Result_image/great");

        cookButton.onClick.AddListener(StartCooking);

        timeImage.SetActive(false);
        greatImage.SetActive(false);
        goodImage.SetActive(false);
        badImage.SetActive(false);

        StartCoroutine(StartCountdown());

        infoScript = FindObjectOfType<Info>();
        seafoodManager = SeafoodManagerNew.Instance;
    }

    void StartCooking()
    {
        Debug.Log(seafoodManager.seafoodCountDict["recipe_numA"]);
        if (isCounting)
        {
            isMoving = !isMoving;
            Debug.Log("StartCooking: isMoving set to " + isMoving);

            if (!isMoving)
            {
                CheckCookingResult();
            }
        }
    }

    IEnumerator StartCountdown()
    {
        isCounting = true;
        int timeLeft = 60;
        while (timeLeft >= 0)
        {
            TimeCount.text = timeLeft.ToString();
            Debug.Log("Countdown: " + timeLeft);
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
        isCounting = false;
        cookButton.interactable = false;

        if (timeImage != null)
        {
            timeImage.SetActive(true);
            Debug.Log("Found timeover image: " + (timeImage != null));
        }
        else
        {
            Debug.LogError("Failed to find 'timeover' image GameObject.");
        }

        isMoving = false;
    }

    void Update()
    {
        if (isMoving)
        {
            float newBarX = MiniGame_bar.transform.position.x + barMoveSpeed;
            if (newBarX < -MoveRange - 2f || newBarX > MoveRange - 2f)
            {
                barMoveSpeed *= -1;
            }
            MiniGame_bar.transform.Translate(Vector3.right * barMoveSpeed);

            float newVerBarX = MiniGame_ver_bar.transform.position.x + verBarMoveSpeed;
            if (newVerBarX < -MoveRange - 2f || newVerBarX > MoveRange - 2f)
            {
                verBarMoveSpeed *= -1;
            }
            MiniGame_ver_bar.transform.Translate(Vector3.right * verBarMoveSpeed);
        }
    }

    void CheckCookingResult()
    {
        Dictionary<string, int> seafoodCountDict = seafoodManager.seafoodCountDict;
        int seafood1Count = seafoodCountDict["seafood1"];
        int seafood2Count = seafoodCountDict["seafood2"];
        int seafood3Count = seafoodCountDict["seafood3"];
        int recipe_numACount = seafoodManager.seafoodCountDict["recipe_numA"];
        int recipe_numBCount = seafoodManager.seafoodCountDict["recipe_numB"];
        int recipe_numCCount = seafoodManager.seafoodCountDict["recipe_numC"];
        
        Debug.Log(seafood1Count == recipe_numACount);
        Debug.Log(seafood1Count);
        Debug.Log(recipe_numACount);
        Debug.Log(seafood2Count == recipe_numBCount);
        Debug.Log(seafood3Count == recipe_numCCount);
        bool isRecipeCorrect = seafood1Count == recipe_numACount &&
                               seafood2Count == recipe_numBCount &&
                               seafood3Count == recipe_numCCount;

        bool isInVerBarRange = MiniGame_bar.transform.localPosition.x + MiniGame_bar.GetComponent<RectTransform>().rect.width / 2 > MiniGame_ver_bar.transform.localPosition.x &&
                               MiniGame_bar.transform.localPosition.x - MiniGame_bar.GetComponent<RectTransform>().rect.width / 2 <= MiniGame_ver_bar.transform.localPosition.x;

        if (isRecipeCorrect)
        {
            if (isInVerBarRange)
            {
                StartCoroutine(ShowResultImage(greatImage));
                Debug.Log("Great");
            }
            else
            {
                StartCoroutine(ShowResultImage(goodImage));
                Debug.Log("Good");
            }
        }
        else
        {
            StartCoroutine(ShowResultImage(badImage));
            Debug.Log("Bad");
        }

        ResetRecipeAndHideInfo();
        Debug.Log("CheckCookingResult completed, ResetRecipeAndHideInfo called");
    }

    IEnumerator ShowResultImage(GameObject image)
    {
        image.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        image.SetActive(false);
    }

    void ResetRecipeAndHideInfo()
    {
        if (infoScript == null)
        {
            infoScript = FindObjectOfType<Info>();
        }

        if (infoScript != null)
        {
            infoScript.HideInfoText();
            Debug.Log("Attempting to hide info texts and reset recipe counts");
        }
        else
        {
            Debug.LogError("Info script not found");
        }

        // Reset recipe counts
        seafoodManager.ResetRecipeCounts();

        if (cookButton != null)
        {
            cookButton.interactable = false;
            Debug.Log("MiniGame_CookButton disabled");
        }
        else
        {
            Debug.LogError("MiniGame_CookButton not found");
        }
    }
}
