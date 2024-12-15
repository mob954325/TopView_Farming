using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    private CanvasGroup canvas;

    private Inventory inventory;

    /// <summary>
    /// 인벤토리 접근용 프로퍼티
    /// </summary>
    public Inventory Inventory { get => inventory; }

    private InventoryContentUI inventoryContent;
    private List<InventorySlotUI> inventorySlots;

    private ContextMenuUI contextMenu;
    private ContextType invenContextType;

    private int slotCount = -1;
    private bool isSlotsCreated = false;

    public InventorySlotUI this[int index]
    {
        get => inventorySlots[index];
    }

    public void Init(int count) 
    {
        if (contextMenu == null) contextMenu = FindAnyObjectByType<ContextMenuUI>();
        if (canvas == null) canvas = GetComponent<CanvasGroup>();
        if (inventoryContent == null) inventoryContent = GetComponentInChildren<InventoryContentUI>();

        slotCount = count;
        CreateSlotUI(slotCount);
    }

    /// <summary>
    /// 슬롯 UI 생성 (한 번만 실행)
    /// </summary>
    /// <param name="count"></param>
    private void CreateSlotUI(int count)
    {
        if (isSlotsCreated)
            return;

        inventorySlots = new List<InventorySlotUI>(slotCount);
        for (int i = 0; i < slotCount; i++)
        {
            GameObject obj = Instantiate(slotPrefab, inventoryContent.transform);
            inventorySlots.Add(obj.GetComponent<InventorySlotUI>());
        }

        inventoryContent.SetHeight(slotCount);

        SetDeactive();
        isSlotsCreated = true;
    }

    private void InitSlots()
    {
        for (int i = 0; i < slotCount; i++)
        {
            InventorySlotUI comp = inventorySlots[i];
            if (comp == null)
            {
                Debug.LogError($"{slotPrefab} 오브젝트에 InventorySlotUI 컴포넌트가 존재하지 않습니다."); // 혹시 모를 컴포넌트 체크
            }

            comp.Init(inventory.Slots[i]);
            comp.OnRightClick += (pointerPosition) =>
            {
                float height = contextMenu.GetComponent<RectTransform>().rect.height;
                int index = comp.Slot.SlotIndex;

                ItemDataSO data = comp.Slot.Data;

                if (data is ItemDataSO_Equipable)
                {
                    contextMenu.OnActive(ContextType.EquipmentSlot, inventory, index, pointerPosition + Vector2.down * height);
                }
                else
                { 
                    contextMenu.OnActive(ContextType.InventorySlot, inventory, index, pointerPosition + Vector2.down * height);
                }

            };
        }
    }

    /// <summary>
    /// UI 패널 활성화, 비활성화 하는 함수
    /// </summary>
    public void ToggleActive(Inventory inven, ContextType contextType)
    {
        if (canvas.alpha < 1f)
        {
            SetActive(inven, contextType);
        }
        else
        {
            SetDeactive();
        }
    }

    public void SetActive(Inventory inven, ContextType contextType)
    {
        inventory = inven;
        invenContextType = contextType;

        InitSlots();
        Inventory.RefreshUI();

        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    public void SetDeactive()
    {
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }
}