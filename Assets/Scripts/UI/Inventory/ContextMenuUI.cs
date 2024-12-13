using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ContextType
{
    None = 0,
    Inventory,  // 인벤토리 클릭 시
    Equipment,          // 장착 장비 클릭 시
    WorldObject     // 월드 오브젝트 클릭 시 (적, 상자)
}

public class ContextMenuUI : MonoBehaviour
{
    private CanvasGroup canvas;
    private RectTransform rectTransform;

    private List<Button> buttons;
    private List<TextMeshProUGUI> buttonTexts;

    private Inventory inventory;
    private int selectedIndex = -1;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGroup>();

        int childCount = transform.childCount;
        buttons = new List<Button>(childCount);
        buttonTexts = new List<TextMeshProUGUI>(childCount);

        Transform child;
        for(int i = 0; i < childCount; i++)
        {
            child = transform.GetChild(i);
            buttons.Add(child.GetComponent<Button>());
            buttonTexts.Add(buttons[i].GetComponentInChildren<TextMeshProUGUI>());
        }
    }

    private void Start()
    {
        OnDeactive();
    }

    public void Init(ContextType type)
    {
        switch (type)
        {
            case ContextType.Inventory:
                SetInventroyButtons();
                break;
            case ContextType.WorldObject:
                SetWorldObjectButtons();
                break;
            case ContextType.Equipment:
                break;
        }
    }

    public void OnActive(Vector2 pos)
    {
        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
        
        SetPosition(pos + Vector2.down * rectTransform.rect.height);
    }

    public void OnActive(Inventory inven, int index, Vector2 pos)
    {
        inventory = inven;
        selectedIndex = index;

        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;

        SetPosition(pos);
    }

    public void OnDeactive()
    {
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    /// <summary>
    /// 모니터 범위 내로 출력할 수 있게 설정하는 함수
    /// </summary>
    /// <param name="pos">커서 위치</param>
    private void SetPosition(Vector2 pos)
    {
        Vector2 resultPos = pos; // pos는 y값이 이미 패널의 높이만큼 빠져있음 (패널 좌측상단이 커서 위치)

        // 패널이 화면 밖으로 나갔는지 확인 후 조정 (나가는 상황이 마지막 슬롯 눌렀을 때만 발생함)
        if(pos.y < 0f)
        {
            //resultPos -= (Vector2.up * pos.y);
            resultPos *= Vector2.right;
        }

        if(pos.x < 0f)
        {
            resultPos *= Vector2.up;
        }

        if(pos.x + rectTransform.rect.width > Screen.width)
        {
            resultPos = new Vector2(Screen.width - rectTransform.rect.width, resultPos.y);
        }

        if(pos.y > Screen.height)
        {
            resultPos = new Vector2(resultPos.x, Screen.height - rectTransform.rect.height);
        }

        rectTransform.anchoredPosition = resultPos;
    }

    private void SetInventroyButtons()
    {
        // 버리기
        buttons[0].gameObject.SetActive(true);
        buttonTexts[0].text = $"Remove";
        buttons[0].onClick.AddListener(() => 
        { 
            inventory.DiscardItems(selectedIndex);
            OnDeactive();
        });

        // 사용
        buttons[1].gameObject.SetActive(true);
        buttonTexts[1].text = $"Use";
        buttons[1].onClick.AddListener(() => { });  // 
    }

    private void SetEquipmentButtons()
    {
        SetInventroyButtons();

        buttons[1].gameObject.SetActive(true);
        buttonTexts[1].text = $"Equip";
        buttons[1].onClick.AddListener(() => { });  // 
    }

    private void SetWorldObjectButtons()
    {
        // 인벤토리 열기
        buttonTexts[0].text = $"Inspection";
        buttons[0].onClick.AddListener(() => { }); // 해당 오브젝트가 가진 인벤토리 리스트 열기

        buttons[1].gameObject.SetActive(false);
    }
}
