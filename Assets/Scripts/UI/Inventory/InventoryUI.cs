using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    private CanvasGroup canvas;

    private Inventory inventory;
    private InventoryContentUI inventoryContent;
    private List<InventorySlotUI> inventorySlots;

    private int slotCount = -1;

    public InventorySlotUI this[int index]
    {
        get => inventorySlots[index];
    }

    private void Awake()
    {
        inventoryContent = GetComponentInChildren<InventoryContentUI>();
        canvas = GetComponent<CanvasGroup>();
    }

    public void Init(Inventory inven) 
    { 
        inventory = inven;

        slotCount = inventory.Slots.Count;
        inventorySlots = new List<InventorySlotUI>(slotCount);   

        for (int i = 0; i < slotCount; i++)
        {
            // 슬롯 내용 초기화
            GameObject obj = Instantiate(slotPrefab, inventoryContent.transform);
            obj.TryGetComponent(out InventorySlotUI comp);

            if (comp == null) Debug.LogError($"{slotPrefab} 오브젝트에 InventorySlotUI 컴포넌트가 존재하지 않습니다."); // 혹시 모를 컴포넌트 체크

            obj.name = $"slot_{i}";
            comp.Init(inventory.Slots[i]);
            inventorySlots.Add(comp);
        }

        inventoryContent.SetHeight(slotCount);
    }

    public void SetActive()
    {
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