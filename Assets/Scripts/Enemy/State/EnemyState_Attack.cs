using System.Collections;
using UnityEngine;

public class EnemyState_Attack : StateBase
{
    private float attackTime = 1f;
    private bool isAttack = false;

    public override void OnEnterState()
    {
        // speed;
        isAttack = false;
        Enemy.Controller.Speed = 1.2f;
        Enemy.Controller.SetDestination(Enemy.Target.transform.position);
    }

    public override void OnExcuting()
    {
        // OnMove
        bool isStop = Enemy.Controller.CheckIsStop();

        if(!isAttack && isStop)
        {
            isAttack = true;

            // 공격
            StartCoroutine(AttackRoutine());

            // 위치 설정 (플레이어)
            Enemy.Controller.SetDestination(Enemy.Target.transform.position);
        }
    }

    public override void OnExitState()
    {
        // stopMoving
        Enemy.Controller.Speed = 0f;
        isAttack = false;
    }

    private IEnumerator AttackRoutine()
    {
        float timeElapsed = 0.0f;
        while(timeElapsed < attackTime)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // 공격 함수 위치 Attack()
        isAttack = false;
    }
}
