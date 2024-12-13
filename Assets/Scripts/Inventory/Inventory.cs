using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private InventoryUI inventoryUI;

    /// <summary>
    /// 슬롯 리스트
    /// </summary>
    private List<InventorySlot> slots;

    /// <summary>
    /// 인벤토리 슬롯 접근용 프로퍼티
    /// </summary>
    public List<InventorySlot> Slots { get => slots; }

    private int maxSlotCount = 10;

    /// <summary>
    /// 인벤토리 생성자
    /// </summary>
    /// <param name="ui">출력할 GUI</param>
    /// <param name="contextType">추가 메뉴 타입</param>
    /// <param name="slotCount">슬롯 개수</param>
    public Inventory(InventoryUI ui, ContextType contextType, int slotCount = 10)
    {
        maxSlotCount = slotCount;
        inventoryUI = ui;

        Init(); 
        inventoryUI.Init(this, contextType);
    }
    
    private void Init()
    {
        slots = new List<InventorySlot>(maxSlotCount);

        for(int i = 0; i < maxSlotCount; i++)
        {
            InventorySlot InventorySlot = new InventorySlot(i);
            slots.Add(InventorySlot);
        }
    }

    /// <summary>
    /// 아이템 추가
    /// </summary>
    /// <param name="Item">추가할 아이템 데이터</param>
    /// <returns>아이템을 성공적으로 획득했으면 true 아니면 false</returns>
    public bool AddItem(ItemDataSO Item)
    {
        bool result = false;

        if (FindRemainSlotIndex() > maxSlotCount)
        {
            // 인벤토리 자리가 없음
            result = false;
        }
        else
        {
            // 남는 슬롯 찾기
            foreach(var slot in slots)
            {
                ItemDataSO data = slot.Data;

                if(data == Item && slot.Count < data.maxCount)
                {
                    // 같은 아이템이고 자리가 남아있으면
                    slot.AddItem(data);
                    inventoryUI[slot.SlotIndex].SetContent(slot);
                    break;
                } 
                else if(data == null)
                {                
                    // 해당 위치의 슬롯의 아이템이 없으면 추가
                    slot.AddItem(Item);
                    inventoryUI[slot.SlotIndex].SetContent(slot);
                    break;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// 아이템 제거 함수
    /// </summary>
    /// <param name="slotIndex">제거할 슬롯 인덱스</param>
    /// <param name="count">제거할 개수</param>
    /// <returns>제거된 아이템 오브젝트 리스트</returns>
    public List<ItemDataSO> DiscardItems(int slotIndex, int count = 1)
    {
        ItemDataSO data = slots[slotIndex].Data;

        // 아이템이 존재하지 않음
        if(data == null)
        {
            return null;
        }

        // 인덱스 범위가 벗어남
        if (slotIndex < 0 || slotIndex > maxSlotCount)
        {
            return null;
        }

        // 아이템 개수가 비정상적임
        if (count < 0 || count > data.maxCount)
        {
            return null;
        }

        List<ItemDataSO> result = new List<ItemDataSO>(data.maxCount);

        for(int i = 0; i < count; i++)
        {
            // 아이템 개수만큼 찾기
            ItemDataSO itemObj = slots[slotIndex].DiscardItem();

            if(itemObj != null)
            {
                result.Add(itemObj);            
            }
            else
            {
                // 아이템 소지 개수보다 더 많은 개수면 break;
                break;
            }
        }

        inventoryUI[slotIndex].SetContent(slots[slotIndex]); // 다 버리고 UI 수정
        // 팩토리에서 아이템 박스 추가하기

        return result;
    }

    /// <summary>
    /// 슬롯 중 데이터가 없는 슬롯 인덱스를 찾는 함수
    /// </summary>
    /// <returns>슬롯 인덱스</returns>
    private int FindRemainSlotIndex()
    {
        int result = 0;

        int curIndex = 0;
        foreach(InventorySlot slot in slots)
        {
            if(slot.Data == null)
            {
                result = curIndex;
                break;
            }

            curIndex++;
        }

        return result;
    }

    public void RefreshUI()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            inventoryUI[i].SetContent(slots[i]);
        }
    }
}