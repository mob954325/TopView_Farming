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

    private int slotCount = -1;

    public InventorySlotUI this[int index]
    {
        get => inventorySlots[index];
    }

    private void Awake()
    {
        contextMenu = FindAnyObjectByType<ContextMenuUI>();
        canvas = GetComponent<CanvasGroup>();
        inventoryContent = GetComponentInChildren<InventoryContentUI>();
    }

    public void Init(Inventory inven, ContextType contextType) 
    { 
        inventory = inven;

        slotCount = inventory.Slots.Count;
        inventorySlots = new List<InventorySlotUI>(slotCount);   

        contextMenu.Init(contextType); // 적이나 상자는 ??

        for (int i = 0; i < slotCount; i++)
        {
            // 슬롯 내용 초기화
            GameObject obj = Instantiate(slotPrefab, inventoryContent.transform);
            obj.TryGetComponent(out InventorySlotUI comp);

            if (comp == null)
            {
                Debug.LogError($"{slotPrefab} 오브젝트에 InventorySlotUI 컴포넌트가 존재하지 않습니다."); // 혹시 모를 컴포넌트 체크
            }

            obj.name = $"slot_{i}";
            comp.Init(inventory.Slots[i]);

            comp.OnRightClick += (pointerPosition) => 
            {
                float height = contextMenu.GetComponent<RectTransform>().rect.height;
                int index = comp.Slot.SlotIndex;
                contextMenu.OnActive(inventory, index, pointerPosition + Vector2.down * height);
            };
            inventorySlots.Add(comp);
        }

        inventoryContent.SetHeight(slotCount);
        SetDeActive();
    }

    public void SetActive()
    {
        Inventory.RefreshUI();

        canvas.alpha = 1f;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    public void SetDeActive()
    {
        canvas.alpha = 0f;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }
}