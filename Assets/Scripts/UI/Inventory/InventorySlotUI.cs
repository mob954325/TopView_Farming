using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    private Image icon;
    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI itemCountText;
    private Button dropButton;

    private InventorySlot slot;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        icon = child.GetComponent<Image>();

        child = transform.GetChild(1);
        itemNameText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        itemCountText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(3);
        dropButton = child.GetComponent<Button>();
    }

    private void SetDataEmpty()
    {
        icon.sprite = null;
        itemNameText.text = $" ";
        itemCountText.text = $" ";
    }

    /// <summary>
    /// 슬롯 초기화 함수
    /// </summary>
    public void Init(InventorySlot currentSlot)
    {
        SetDataEmpty();
        // dropButton.onClick.AddListener(); // TODO : 아이템 버리기 이벤트 추가하기

        slot = currentSlot;
    }

    /// <summary>
    /// 슬롯 UI 갱신 함수
    /// </summary>
    /// <param name="slot">갱신할 슬롯</param>
    public void SetContent(InventorySlot slot)
    {
        if(slot.Data == null)
        {
            SetDataEmpty();
        }
        else
        {
            icon.sprite = slot.Data.Icon;
            itemNameText.text = slot.Data.name;
            itemCountText.text = $"{slot.Count}";
        }
    }
}