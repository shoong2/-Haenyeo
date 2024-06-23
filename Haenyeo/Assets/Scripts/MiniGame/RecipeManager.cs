using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    public GameObject iconPrefab; // 아이콘의 프리팹
    public Transform iconParent; // 아이콘들을 배치할 부모 객체
    public Info info; // Info 스크립트 참조

    public GameObject removeFishParent; // remove_fish 부모 객체
    public GameObject[] removeIcons; // remove 아이콘들 배열

    public GameObject Crust_tuto_A_Button; // Crust_tuto_A 버튼
    public GameObject Crust_tuto_B_Button; // Crust_tuto_B 버튼
    public GameObject Crust_tuto_C_Button; // Crust_tuto_C 버튼

    // Start is called before the first frame update
    void Start()
    {
        // 초기에는 아이콘들을 비활성화
        SetIconsActive(false);
        
        // remove 버튼들을 비활성화
        SetRemoveButtonsActive(false);

        // Crust_tuto_A 버튼 클릭 이벤트 추가
        Button crustTutoAButton = Crust_tuto_A_Button.GetComponent<Button>();
        crustTutoAButton.onClick.AddListener(() => EnableRemoveButtons(0)); // 0은 remove1 버튼의 인덱스를 나타냄

        // Crust_tuto_B 버튼 클릭 이벤트 추가
        Button crustTutoBButton = Crust_tuto_B_Button.GetComponent<Button>();
        crustTutoBButton.onClick.AddListener(() => EnableRemoveButtons(1)); // 1은 remove2 버튼의 인덱스를 나타냄

        // Crust_tuto_C 버튼 클릭 이벤트 추가
        Button crustTutoCButton = Crust_tuto_C_Button.GetComponent<Button>();
        crustTutoCButton.onClick.AddListener(() => EnableRemoveButtons(2)); // 2은 remove3 버튼의 인덱스를 나타냄

        // 레시피 아이템을 클릭하는 이벤트를 추가
        Button recipeItemButton = GameObject.Find("RecipeItem").GetComponent<Button>();
        recipeItemButton.onClick.AddListener(()=>{info.gameObject.SetActive(true);});
        recipeItemButton.onClick.AddListener(ShowIcons);
        recipeItemButton.onClick.AddListener(EnableCrustTutoButtons);

        // Info 스크립트의 참조를 할당합니다.
        if (info == null)
        {
            Debug.LogError("Info 스크립트를 찾을 수 없습니다.");
        }

        // remove 아이콘들을 배열에 저장
        removeIcons = new GameObject[removeFishParent.transform.childCount];
        for (int i = 0; i < removeFishParent.transform.childCount; i++)
        {
            removeIcons[i] = removeFishParent.transform.GetChild(i).gameObject;
            removeIcons[i].SetActive(false); // 처음에는 보이지 않도록 설정
        }
    }

    // 아이콘들을 활성화하고 위치를 설정하는 함수
    void SetIconsActive(bool active)
    {
        for (int i = 0; i < iconParent.childCount; i++)
        {
            iconParent.GetChild(i).gameObject.SetActive(active);
        }
    }

    // remove 버튼들을 활성화하거나 비활성화하는 함수
    void SetRemoveButtonsActive(bool active)
    {
        foreach (GameObject icon in removeIcons)
        {
            icon.SetActive(active);
        }
    }

    // Crust_tuto_A,B,C 버튼을 활성화하는 함수
    void EnableCrustTutoButtons()
    {
        // Crust_tuto_A,B,C 버튼 활성화
        Crust_tuto_A_Button.SetActive(true);
        Crust_tuto_B_Button.SetActive(true);
        Crust_tuto_C_Button.SetActive(true);

        // remove 버튼들은 처음에는 비활성화
        //SetRemoveButtonsActive(false);
    }

    // remove 버튼들을 활성화하는 함수
    void EnableRemoveButtons(int removeIndex)
    {
        // 먼저 모든 remove 버튼을 비활성화
        SetRemoveButtonsActive(false);

        // 선택한 remove 버튼을 활성화
        removeIcons[removeIndex].SetActive(true);

        // 선택한 remove 버튼을 제외하고 비활성화
        for (int i = 0; i < removeIcons.Length; i++)
        {
            if (i != removeIndex)
            {
                removeIcons[i].SetActive(false);
            }
        }
    }

    // 레시피 아이템을 클릭했을 때 호출되는 함수
    void ShowIcons()
    {
        // 여기에서는 간단히 아이콘 A, B, C를 생성하고 배치하도록 했습니다.
        CreateIcon("IconA", new Vector3(-1.65f, -2.41f, -63.97432f ));
        CreateIcon("IconB", new Vector3(0.97f, -2.45f, -63.97432f));
        CreateIcon("IconC", new Vector3(3.56f, -2.41f, -63.97432f));

        // 아이콘들을 활성화
        SetIconsActive(true);

        // Info의 정보 업데이트
        info.UpdateInfoText();
    }

    // 아이콘을 생성하고 위치를 설정하는 함수
    void CreateIcon(string iconName, Vector3 position)
    {
        GameObject newIcon = Instantiate(iconPrefab, iconParent);
        newIcon.name = iconName;
        newIcon.transform.localPosition = position;
    }
}
