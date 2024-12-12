#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_06_UI : TestBase
{
    public Player player;
    public HealthBar playerHealth;

    private void Start()
    {
        playerHealth.Init(player);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.Hit(1);
    }
}
#endif 