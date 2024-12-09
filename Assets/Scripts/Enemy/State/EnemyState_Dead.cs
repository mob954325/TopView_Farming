using System.Collections;
using UnityEngine;

public class EnemyState_Dead : StateBase
{
    public override void OnEnterState()
    {
        Enemy.Controller.Speed = 0f;
        Enemy.Controller.SetStop(true);
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
