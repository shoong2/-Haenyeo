using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    //[SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] TMP_Text txt_Dialogue;
    [SerializeField] TMP_Text txt_Name;

    Dialogue[] dialogues;

    bool isDialogue = false;
    bool isNext = false; // 특정 키 입력 대기.

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0; // 대화 카운트
    int contextCount = 0; // 대화 카운트

    [Header("저장소")]
    [SerializeField] SaveNLoad storage;
    //public string tagName;

    [Header("알림창")]
    [SerializeField] GameObject rewardBox;
    //[SerializeField] Sprite itemSprite;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDetail;

    [SerializeField] GameObject questBox;
    [SerializeField] TMP_Text questName;
    bool isClickRewardBox = false;
    bool isClickQuestBox = false;

    bool multiCoroutine = false;

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogues;
        StartCoroutine(Typewriter());
    }

    IEnumerator Typewriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");

        //txt_Dialogue.text = t_ReplaceText;
        txt_Name.text = dialogues[lineCount].name;
        for(int i=0; i<t_ReplaceText.Length; i++)
        {
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }
        isNext = true;

        
    }

    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        //go_DialogueNameBar.SetActive(p_flag);
    }

    private void Update()
    {

        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isNext = false;
                    txt_Dialogue.text = "";
                    if(++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(Typewriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if(++lineCount<dialogues.Length)
                        {
                            StartCoroutine(Typewriter());
                        }
                        else
                        {
                            EndDialogue();
                        }
                    }
                }
            }
        }

  

        if (Input.GetMouseButtonDown(0) && !isDialogue)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if(hit.collider !=null)
            {
                Debug.Log(storage.saveData.nowIndex);
                if (SetSystemIndex(0, hit, "Won", "Room"))
                {
                    ShowDialogue(DatabaseManager.instance.GetDialogue(1, 9));
                }
                else if(SetSystemIndex(1,hit,"Won","Sea"))
                {
                    ShowDialogue(DatabaseManager.instance.GetDialogue(10, 14));
                }
                //hit.transform.GetComponent<InteractionEvent>().GetDialogue();
                //SettingUI(true);
                //ShowDialogue(hit.transform.GetComponent<InteractionEvent>().GetDialogue());
                
            }
        }

        
    }

    public void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        SettingUI(false);
        if(storage.saveData.nowIndex==0)
        {
            GetReward(0);
        }
        else if(storage.saveData.nowIndex == 1)
        {
            GetReward(1);
        }
        storage.saveData.nowIndex++;
        storage.SaveData();
    }

    bool SetSystemIndex(int index, RaycastHit2D hit, string character, string SceneName)
    {
        if (storage.saveData.nowIndex == index && hit.collider.tag == character
            && SceneManager.GetActiveScene().name == SceneName)
        {
            return true;
        }
        else
            return false;
    }

    void GetReward(int index)
    {
        if(index ==0)
        {
            StartCoroutine(ShowRewardBox(new string[] {"초보 헤녀의 고무옷", "초보 해녀의 수경", "초보 해녀의 오리발"}));
            StartCoroutine(ShowQuestBox(new string[] { "해녀복 착용하기", "바다로 가보기" }));
        }
        else if(index ==1)
        {
            StartCoroutine(ShowRewardBox(new string[] { "초보 헤녀의 칼", "초보 해녀의 호미", "초보 해녀의 작살" }));
            StartCoroutine(ShowQuestBox(new string[] { "도구 착용하기", "바다에서 3m까지 버티기" }));
        }
    }

    IEnumerator ShowRewardBox(string[] name)
    {
        for(int i=0; i<name.Length; i++)
        {
            itemName.text = name[i];
            itemDetail.text = "'" + name[i] + "'" + " 을 획득했습니다!";
            rewardBox.SetActive(true);
            if(i!=name.Length-1)
                yield return new WaitUntil(() => isClickRewardBox);
            isClickRewardBox = false;
        }
        multiCoroutine = true;
    }

    IEnumerator ShowQuestBox(string[] quest)
    {
        yield return new WaitUntil(() => multiCoroutine && isClickRewardBox);
        for (int i = 0; i < quest.Length; i++)
        {
            questName.text = quest[i];
            questBox.SetActive(true);
            if (i != quest.Length - 1)
                yield return new WaitUntil(() => isClickQuestBox);
            isClickQuestBox = false;
        }
        multiCoroutine = false;
    }

    public void ClickRewardBox()
    {
        isClickRewardBox = true;
        rewardBox.SetActive(false);

    }

    public void ClickQuestBox()
    {
        isClickQuestBox = true;
        questBox.SetActive(false);
    }    
}
