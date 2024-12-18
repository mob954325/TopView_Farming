using System.Collections;
using UnityEngine;

public class EnemyState_Attack : StateBase
{
    private EnemyAttackArea attackArea;
    protected EnemyBase enemy;

    private bool isAttack = false;

    public override void Init()
    {
        enemy = GetComponentInParent<EnemyBase>();
        attackArea = transform.parent.GetChild(2).GetComponent<EnemyAttackArea>();
    }


    public override void OnEnterState()
    {
        isAttack = false;
        enemy.Controller.Speed = 1.2f;
        enemy.Controller.SetStop(false);
        enemy.Controller.SetDestination(enemy.Target.transform.position);
    }

    public override void OnExcuting()
    {
        // 위치 설정 (플레이어)
        enemy.Controller.SetDestination(enemy.Target.transform.position);

        bool isStop = enemy.Controller.CheckIsStop();
        Vector3 lookAtVec = Vector3.Lerp(transform.forward, enemy.Target.gameObject.transform.position, Time.deltaTime);
        transform.LookAt(lookAtVec);

        if (!enemy.CheckPlayerInAttackArea())
        {
            enemy.State = EnemyState.Idle;
        }
        else
        {
            if (enemy.CheckPlayerInSight())
            {
                if (!isAttack && isStop)
                {
                    isAttack = true;

                    // 공격
                    StartCoroutine(AttackDelay());
                }
            }
        }
    }

    private IEnumerator AttackDelay()
    {
        float timeElapsed = 0.0f;

        attackArea.SetColliderActive(true);

        while (timeElapsed < enemy.AttackRatePerSec)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        attackArea.SetColliderActive(true);
        isAttack = false;
    }

    public override void OnExitState()
    {
        // stopMoving
        enemy.Controller.Speed = 0f;
        isAttack = false;
        StopAllCoroutines();
        attackArea.SetColliderActive(false);
    }
}
