#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_07_Item : TestBase
{
    public Player player;
    public LocalManager manager;
    public CombinationRecipes recipes;

    [Range(0,15)]
    public int index1 = 0;
    [Range(0,15)]
    public int index2 = 0;  

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        manager = FindAnyObjectByType<LocalManager>();
        recipes = FindAnyObjectByType<CombinationRecipes>();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.Inventory.AddItem(manager.ItemDataManager.Items[(int)ItemCode.Scrap]);
        player.Inventory.AddItem(manager.ItemDataManager.Items[(int)ItemCode.Damanged_Equipment]);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        /*InventorySlot item1 = player.Inventory.Slots[index1];
        InventorySlot item2 = player.Inventory.Slots[index2];

        if (item1 == null || item2 == null) // 둘 중 하나라도 아이템이 없으면 종료
            return;

        ItemCode valueCode = recipes.GetRecipeItem(item1.Data.code, item2.Data.code);

        if (valueCode != ItemCode.None)
        {
            player.Inventory.Slots[index1].DiscardItem();
            player.Inventory.Slots[index2].DiscardItem();

            player.Inventory.AddItem(manager.ItemDataManager.Items[(int)valueCode]);
        }*/

        player.Inventory.AddItem(manager.ItemDataManager.Items[(int)ItemCode.RedStick]);
    }
}
#endif