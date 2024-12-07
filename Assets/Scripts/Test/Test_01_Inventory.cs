#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_01_Inventory : TestBase
{
    public Inventory inven;
    private Transform spawnPos;

    public ItemDataSO data;

    public int index = 0;

    private void Start()
    {
        inven = new Inventory();
        spawnPos = transform.GetChild(0);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        inven.AddItem(data);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        inven.DiscardItem(index);
    }
}
#endif
