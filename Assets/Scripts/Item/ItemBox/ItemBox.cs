using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemBox : Product, IInteractable
{
    private Inventory inventory;
    public Inventory Inventory { get => inventory; }

    private InventoryUI inventoryUI;
    private ContextMenuUI contextMenu;

    public bool canInteract = false;
    public bool CanInteract { get => canInteract; set => canInteract = value; }

    public ContextType contextType;

    public void Init()
    {
        inventoryUI = FindAnyObjectByType<InventoryUI>();
        contextMenu = FindAnyObjectByType<ContextMenuUI>();

        inventory = new Inventory(inventoryUI); // ...
        inventory.OnDiscardItem += () => 
        {
            if (inventory.CheckIsInventoryEmpty())
            {
                inventory.InventoryUI.SetDeactive();
                this.gameObject.SetActive(false);
            }
        };

        CanInteract = true;
    }

    public void OnInteract()
    {
        // context메뉴가 열리고 
        // 메뉴 내용 초기화
        LocalManager manager = FindAnyObjectByType<LocalManager>();
        float distanceSqr = Vector3.SqrMagnitude(this.gameObject.transform.position - manager.Player.transform.position);

        if(distanceSqr < 2.5f)
        {
            contextMenu.OnActive(ContextType.WorldObject, inventory, Mouse.current.position.value);
        }
        else
        {
            contextMenu.OnDeactive();
        }
    }
}