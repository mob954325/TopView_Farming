#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_01_Inventory : TestBase
{
    public Inventory inven;
    private Transform spawnPos;

    public ItemDataSO data;

    public int index = 0;

    [Range(1, 3)]
    public int count = 1;

    private void Start()
    {
        inven = new Inventory(null);
        spawnPos = transform.GetChild(0);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        inven.AddItem(data);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        List<ItemDataSO> items = inven.DiscardItems(index, count);

        if (items != null)
        {
            foreach (var item in items)
            {
                GameObject curObj = Instantiate(item.prefab);
                curObj.transform.position = spawnPos.position;
            }
        }
    }
}
#endif
