using System.Collections;
using UnityEngine;

public class EnemyState_Dead : StateBase
{
    protected EnemyBase enemy;

    public override void Init()
    {
        enemy = GetComponentInParent<EnemyBase>();
    }

    public override void OnEnterState()
    {
        enemy.Controller.Speed = 0f;
        enemy.Controller.SetStop(true);
    }

    public override void OnExcuting()
    {
        // 사용 안함
    }

    public override void OnExitState()
    {
        // 사용 안함
    }

    private IEnumerator DeadProcess()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}
