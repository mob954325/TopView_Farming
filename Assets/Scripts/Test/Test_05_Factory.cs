#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_05_Factory : TestBase
{
    public Player player;
    public Factory_Enemy enemyFactory;
    public Factory_ItemBox boxFactory;
    public ItemBox box;

    public Transform spawn;
    public ItemDataSO testItem;

    [Header("ItemBox")]
    public int index = 0;
    public int count = 1;

    private void Start()
    {
        if (player == null) player = FindAnyObjectByType<Player>();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        EnemyBase enemy = enemyFactory.SpawnEnemy(spawn.position, spawn.rotation);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        // 플레이어가 아이템 버리기

        List<ItemDataSO> items = player.Inventory.DiscardItems(index, count);

        if (items != null)
        {
            boxFactory.SpawnBox(items, spawn.position, spawn.rotation);
        }
        else
        {
            Debug.Log($"플레이어 {index}번째에 아이템이 없습니다.");
        }
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        // 박스에서 아이템 가져가기
        box = FindAnyObjectByType<ItemBox>();

        List<ItemDataSO> items = box.RemoveItem(index, count);

        if (items != null)
        {
            foreach (ItemDataSO item in items)
            {
                player.Inventory.AddItem(item);
            }
        }
        else
        {
            Debug.Log($"박스에 {index}번째에 아이템이 없습니다.");
        }
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        for (int i = 0; i < 4; i++)
        {
            player.Inventory.AddItem(testItem);
        }
    }
}
#endif