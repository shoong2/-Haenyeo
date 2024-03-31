using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    public GameObject iconPrefab; // 아이콘의 프리팹
    public Transform iconParent; // 아이콘들을 배치할 부모 객체

    // Start is called before the first frame update
    void Start()
    {
        // 초기에는 아이콘들을 비활성화
        SetIconsActive(false);
        
        // 레시피 아이템을 클릭하는 이벤트를 추가
        Button recipeItemButton = GameObject.Find("RecipeItem").GetComponent<Button>();
        recipeItemButton.onClick.AddListener(ShowIcons);
    }

    // 아이콘들을 활성화하고 위치를 설정하는 함수
    void SetIconsActive(bool active)
    {
        for (int i = 0; i < iconParent.childCount; i++)
        {
            iconParent.GetChild(i).gameObject.SetActive(active);
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
    }

    // 아이콘을 생성하고 위치를 설정하는 함수
    void CreateIcon(string iconName, Vector3 position)
    {
        GameObject newIcon = Instantiate(iconPrefab, iconParent);
        newIcon.name = iconName;
        newIcon.transform.localPosition = position;
    }
}