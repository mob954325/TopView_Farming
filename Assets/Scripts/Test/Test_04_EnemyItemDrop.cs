#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_EnemyItemDrop : TestBase
{
    public Player player;
    public EnemyBase enemy;

    public int index;

    private void Start()
    {
        player = FindAnyObjectByType<Player>(); 
        enemy = FindAnyObjectByType<EnemyBase>(FindObjectsInactive.Include);

        enemy.gameObject.SetActive(true);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        if(enemy.Health <= 0f)
        {
            ItemDataSO itemData = enemy.GetItem(index);

            if(itemData != null)
            {
                player.Inventory.AddItem(itemData);
            }
            else
            {
                Debug.Log($"적 드랍 테이블의 {index}번 아이템이 존재하지 않습니다.");
            }
        }
        else
        {
            Debug.Log("적이 생존해있습니다.");
        }
    }
}
#endif