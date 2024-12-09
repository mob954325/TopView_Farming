using System.Collections;
using UnityEngine;

public class EnemyState_Attack : StateBase
{
    private EnemyAttackArea attackArea;

    private bool isAttack = false;

    private void Start()
    {        
        attackArea = transform.parent.GetChild(2).GetComponent<EnemyAttackArea>();
    }

    public override void OnEnterState()
    {
        // speed;
        isAttack = false;
        Enemy.Controller.Speed = 1.2f;
        Enemy.Controller.SetStop(false);
        Enemy.Controller.SetDestination(Enemy.Target.transform.position);
    }

    public override void OnExcuting()
    {
        // 위치 설정 (플레이어)
        Enemy.Controller.SetDestination(Enemy.Target.transform.position);

        bool isStop = Enemy.Controller.CheckIsStop();
        Vector3 lookAtVec = Vector3.Lerp(transform.forward, Enemy.Target.gameObject.transform.position, Time.deltaTime);
        transform.LookAt(lookAtVec);

        if (!Enemy.CheckPlayerInAttackArea())
        {
            Enemy.State = EnemyState.Idle;
        }
        else
        {
            if (Enemy.CheckPlayerInSight())
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

        while (timeElapsed < Enemy.AttackRatePerSec)
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
        Enemy.Controller.Speed = 0f;
        isAttack = false;
    }
}
