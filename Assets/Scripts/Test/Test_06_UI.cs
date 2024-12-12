#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_06_UI : TestBase
{
    public Factory_Enemy factory_Enemy;

    public Player player;
    public HealthBar playerHealth;

    public ItemDataSO data;
    public InventoryUI invenUI;

    private Transform spawnPosition;

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
        player.Inventory.RemoveItems(0);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        invenUI.SetActive();
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        invenUI.SetDeActive();
    }

}
#endif 