using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ContextType
{
    None = 0,
    InventorySlot,          // 플레이어 인벤토리 클릭 시
    EquipmentSlot,          // 장착 장비 클릭 시
    PlaceableSlot,          // 설치 아이템 클릭 시
    WorldObject,            // 월드 오브젝트 클릭 시 (적, 상자)
    WorldObjectInventory    // 플레이어 외 오브젝트의 인벤토리 클릭 시

}

public class ContextMenuUI : MonoBehaviour
{
    private Player player;
    private Factory_ItemBox factory_ItemBox;
    private CombinationPanelUI combinationUI;

    private CanvasGroup canvas;
    private RectTransform rectTransform;

    private List<Button> buttons;
    private List<TextMeshProUGUI> buttonTexts;

    private Inventory inventory;
    private int selectedIndex = -1;

    private void Start()
    {
        Init();
        OnDeactive();
    }

    public void Init()
    {
        player = FindAnyObjectByType<Player>();
        factory_ItemBox = FindAnyObjectByType<Factory_ItemBox>();
        combinationUI = FindAnyObjectByType<CombinationPanelUI>();

        // 컴포넌트 찾기
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGroup>();

        int childCount = transform.childCount;
        buttons = new List<Button>(childCount);
        buttonTexts = new List<TextMeshProUGUI>(childCount);

        Transform child;
        for (int i = 0; i < childCount; i++)
        {
            child = transform.GetChild(i);
            buttons.Add(child.GetComponent<Button>());
            buttonTexts.Add(buttons[i].GetComponentInChildren<TextMeshProUGUI>());
        }
    }

    private void SetButton(ContextType type)
    {
        switch (type)
        {
            case ContextType.InventorySlot:
                SetInventorySlotButtons();
                break;
            case ContextType.PlaceableSlot:

                break;
            case ContextType.EquipmentSlot:
                SetEquipmentSlotButtons();
                break;
            case ContextType.WorldObject:
                SetWorldObjectButtons();
                break;
            case ContextType.WorldObjectInventory:
                SetWorldObjectInventory();
                break;
        }
    }

    /// <summary>
    /// ContextUI 활성화
    /// </summary>
    public void OnActive(ContextType type, Inventory inven, Vector2 pos)
    {
        inventory = inven;

        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
        
        SetButton(type);
        SetPosition(pos + Vector2.down * rectTransform.rect.height);
    }

    /// <summary>
    /// ContextUI 활성화
    /// </summary>
    public void OnActive(ContextType type, Inventory inven, int index, Vector2 pos)
    {
        inventory = inven;
        selectedIndex = index;

        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;

        SetButton(type);
        SetPosition(pos);
    }

    /// <summary>
    /// ContextMenuUI 비활성화
    /// </summary>
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

    // ContextMenu 설정 함수 ===================================================================================
    
    /// <summary>
    /// 0번째 버튼 아이템 제거로 설정하는 함수
    /// </summary>
    private void SetInventorySlotRemove()
    {
        // 버리기
        buttons[0].gameObject.SetActive(true);
        buttonTexts[0].text = $"Remove";
        buttons[0].onClick.AddListener(() =>
        {
            List<ItemDataSO> datas = inventory.DiscardItems(selectedIndex);
            Vector3 randVec = player.transform.position + Random.onUnitSphere;
            randVec = new Vector3(randVec.x, 0.5f, randVec.z);

            factory_ItemBox.SpawnBox(datas, randVec, Quaternion.identity);
            inventory.RefreshUI();
            OnDeactive();
        });
    }
    
    private void ButtonsListenerReset()
    {
        foreach(Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void SetInventorySlotButtons()
    {
        ButtonsListenerReset();
        SetInventorySlotRemove();

        // 사용
        buttons[1].gameObject.SetActive(true);
        buttonTexts[1].text = $"Add Combination";
        buttons[1].onClick.AddListener(() => 
        {
            combinationUI.SetActive();
            bool isSuccess = combinationUI.SetData(inventory.Slots[selectedIndex].Data, selectedIndex);

            if(!isSuccess)
            {
                Debug.Log("조합창 가득참");
            }

            OnDeactive();
        });
    }

    private void SetEquipmentSlotButtons()
    {
        ButtonsListenerReset();
        SetInventorySlotRemove();

        buttons[1].gameObject.SetActive(true);
        if(player.WeaponData != null)
        {
            buttonTexts[1].text = $"UnEquip";
            buttons[1].onClick.AddListener(() =>
            {
                player.UnEquipWeapon();
                OnDeactive();
            });
        }
        else
        {
            buttonTexts[1].text = $"Equip";
            buttons[1].onClick.AddListener(() =>
            {
                player.EquipWeapon(inventory.Slots[selectedIndex].Data as ItemDataSO_Equipable);
                OnDeactive();
            });
        }
    }

    private void SetPlaceableSlotButtons()
    {
        buttons[1].gameObject.SetActive(true);

        buttonTexts[1].text = $"Place";
        buttons[1].onClick.AddListener(() =>
        {
            // 설치
            OnDeactive();
        });
    }

    private void SetWorldObjectButtons()
    {
        ButtonsListenerReset();

        // 인벤토리 열기
        buttonTexts[0].text = $"Inspection";
        buttons[0].onClick.AddListener(() => 
        {
            // 해당 오브젝트가 가진 인벤토리 리스트 열기
            inventory.InventoryUI.SetActive(inventory,ContextType.WorldObjectInventory);
            OnDeactive();
        }); 

        buttons[1].gameObject.SetActive(false);
    }

    private void SetWorldObjectInventory()
    {
        ButtonsListenerReset();

        buttonTexts[0].text = $"Get";
        buttons[0].onClick.AddListener(() =>
        {
            // 플레이어 인벤토리에 아이템 추가,
            // 지금 출력된 인벤토리에 아이템 제거
            List<ItemDataSO> datas = inventory.DiscardItems(selectedIndex);

            foreach(ItemDataSO item in datas)
            {
                player.Inventory.AddItem(item);
            }

            inventory.RefreshUI();
            OnDeactive();
        });

        buttons[1].gameObject.SetActive(false);
    }
}
