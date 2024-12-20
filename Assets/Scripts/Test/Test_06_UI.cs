#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_06_UI : TestBase
{
    public Factory_Enemy factory_Enemy;
    public Factory_ItemBox factory_ItemBox;

    public Player player;
    public HealthBar playerHealth;

    public ItemDataSO data;
    public InventoryUI invenUI;

    private Transform spawnPosition;

    [Range(0, 10)]
    public int index = 0;

    private void Start()
    {
        playerHealth.Init(player);
        spawnPosition = transform.GetChild(0);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.Hit(1);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        player.Inventory.AddItem(data);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        player.Inventory.DiscardItems(index);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        List<ItemDataSO> temp = new List<ItemDataSO> { data };
        factory_Enemy.SpawnEnemy(spawnPosition.position, spawnPosition.rotation);
        factory_ItemBox.SpawnBox(temp, spawnPosition.position, spawnPosition.rotation);
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {

    }

}
#endif 