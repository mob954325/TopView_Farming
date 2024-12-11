#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_05_Factory : TestBase
{
    public Factory_Enemy factory;
    public Transform spawn;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        EnemyBase enemy = factory.SpawnEnemy(spawn.position, spawn.rotation);
    }
}
#endif