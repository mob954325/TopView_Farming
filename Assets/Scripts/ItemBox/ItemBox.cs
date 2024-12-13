using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemBox : Product, IInteractable
{
    private Inventory inventory;
    public Inventory Inventory { get => inventory; }

    private bool canInteract = false;
    public bool CanInteract { get => canInteract; set => canInteract = value; }

    public ContextType contextType;
    public int capacity = 10;

    public void Init()
    {
        inventory = new Inventory(null, contextType, capacity); // ...
        CanInteract = true;
    }

    public void AddItems(List<ItemDataSO> datas)
    {
        foreach (ItemDataSO item in datas)
        {
            inventory.AddItem(item);
        }
    }

    public List<ItemDataSO> RemoveItem(int index, int count)
    {
        List<ItemDataSO> result = Inventory.DiscardItems(index, count);

        if(CheckRemainItem())
        {
            this.gameObject.SetActive(false); // 아이템이 없으면 상자 비활성화
        }

        return result;
    }

    private bool CheckRemainItem()
    {
        bool result = true;

        foreach(InventorySlot slot in inventory.Slots)
        {
            if(slot.Data != null)
            {
                result = false;
                break;
            }
        }

        return result;
    }
}