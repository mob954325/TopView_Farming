#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_03_PlayerAttack : TestBase
{
    public Player player;
    public ItemDataSO_Equipable weapon;

    public void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        if(weapon != null)
        {
            player.EquipWeapon(weapon);
        }
        else
        {
            Debug.Log("아이템 데이터가 없습니다.");
        }
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        player.UnEquipWeapon();
    }
}
#endif