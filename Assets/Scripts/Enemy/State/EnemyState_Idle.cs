using UnityEngine;

public class EnemyState_Idle : StateBase
{
    protected EnemyBase enemy;

    public float timer = 0;
    public float maxTimer = 1f;

    public override void Init()
    {
        enemy = GetComponentInParent<EnemyBase>();
    }

    public override void OnEnterState()
    {
        enemy.Controller.SetStop(false);
        enemy.Controller.Speed = 0.8f;
    }

    public override void OnExcuting()
    {
        bool isStop = enemy.Controller.CheckIsStop();

        if(enemy.CheckPlayerInAttackArea())
        {
            enemy.State = EnemyState.Attack;
        }
        else
        {
            if (timer < maxTimer)
            {
                timer += Time.deltaTime;
            }
            else
            {
                // 타이머 초기화
                maxTimer = Random.Range(1.5f, 4f);
                timer = 0f;
                enemy.Controller.SetStop(false);
            }

            if (isStop)
            {
                // 다음 이동 위치 선정
                Vector3 nextVec = SetRandomNextDestination();
                enemy.Controller.SetDestination(nextVec);
                enemy.Controller.SetStop(true);
            }
        }        
    }

    public override void OnExitState()
    {
        // endLookAround
        enemy.Controller.Speed = 0f;
        timer = 0f;
        enemy.Controller.SetStop(true);
    }

    private Vector3 SetRandomNextDestination()
    {
        Vector3 nextPosition = Random.onUnitSphere * 2f;
        return nextPosition;
    }
}
