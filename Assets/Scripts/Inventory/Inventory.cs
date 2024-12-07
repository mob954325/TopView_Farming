using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    /// <summary>
    /// 슬롯 리스트
    /// </summary>
    List<Inventory_Slot> slots;

    private int maxSlotCount = 10;

    public Inventory(int slotCount = 10)
    {
        maxSlotCount = slotCount;
        Init();
    }
    
    private void Init()
    {
        slots = new List<Inventory_Slot>(maxSlotCount);
    }

    /// <summary>
    /// 아이템 추가
    /// </summary>
    /// <param name="Item">추가할 아이템 데이터</param>
    /// <returns>아이템을 성공적으로 획득했으면 true 아니면 false</returns>
    public bool AddItem(ItemDataSO Item)
    {
        bool result = false;

        int remainIndex = FindRemainSlotIndex();
        if (remainIndex == -1)
        {
            // 인벤토리 자리가 없음
            result = false;
        }
        else
        {
            // 아이템 추가
            slots[remainIndex].AddItem(Item);
        }

        return result;
    }

    /// <summary>
    /// 아이템 제거 함수
    /// </summary>
    /// <param name="slotIndex">제거할 슬롯 인덱스</param>
    /// <param name="count">제거할 개수</param>
    /// <returns>제거된 아이템 오브젝트 리스트</returns>
    public List<GameObject> DiscardItem(int slotIndex, int count = 1)
    {
        ItemDataSO data = slots[slotIndex].Data;
        List<GameObject> result = new List<GameObject>(data.maxCount);

        // 예외 처리
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

        for(int i = 0; i < count; i++)
        {
            result.Add(slots[slotIndex].DiscardItem());            
        }

        return result;
    }

    /// <summary>
    /// 슬롯 중 데이터가 없는 슬롯 인덱스를 찾는 함수 (슬롯이 없으면 -1을 반환)
    /// </summary>
    /// <returns>슬롯 인덱스</returns>
    private int FindRemainSlotIndex()
    {
        int result = -1;

        int curIndex = 0;
        foreach(Inventory_Slot slot in slots)
        {
            if(slot.Data == null)
            {
                result = curIndex;
            }

            curIndex++;
        }

        return result;
    }
}