using UnityEngine;

public class Inventory_Slot
{
    private ItemDataSO data;
    public ItemDataSO Data { get => data; private set => data = value; }

    private int maxCount = 0;

    private int count = 0;
    public int Count 
    { 
        get => count;
        private set
        {
            count = value;

            if(count <= 0)  // 아이템이 완전히 없어지면
            {
                Data = null; // 데이터 제거
            }
        }
    }

    public Inventory_Slot()
    {
        Data = null;
        count = 0;
        maxCount = 0;
    }

    public bool AddItem(ItemDataSO itemData)
    {
        bool result = false;

        if(Data != null)
        {
            // 아이템이 이미 있다.
            if(itemData == Data && Count < maxCount)
            {
                // 같은 아이템이면 추가
                count++;
                result = true;
            }
            else
            {
                // 아이템이 다르거나 꽉참
                result = false;
            }
        }
        else
        {
            // 빈 상태에서 아이템 추가
            Data = itemData;
            maxCount = Data.maxCount;
            Count++;

            result = true;
        }

        return result;
    }

    public ItemDataSO DiscardItem()
    {
        ItemDataSO result = null;

        if(Count <= 0)
        {
            // 아이템이 없으면 null 반환
            result = null;
        }
        else
        {
            result = Data;
            Count--;
        }


        return result;
    }

    public void DeleteData()
    {
        Data = null;
        maxCount = 0;
        Count = 0;
    }
}