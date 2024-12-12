using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    private Image icon;
    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI itemCountText;

    private InventorySlot slot;
    /// <summary>
    /// 슬롯 접근용 프로퍼티
    /// </summary>
    public InventorySlot Slot { get => slot; }

    /// <summary>
    /// 추가 메뉴 패널 생성용 델리게이트 (파라미터 : 마우스 rect 위치 값)
    /// </summary>
    public Action<Vector2> OnRightClick;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        icon = child.GetComponent<Image>();

        child = transform.GetChild(1);
        itemNameText = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(2);
        itemCountText = child.GetComponent<TextMeshProUGUI>();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slot.Data == null) // 아이템 정보 없으면 무시
            return;

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            Vector2 pos = eventData.position;
            OnRightClick?.Invoke(pos);
        }
    }
}