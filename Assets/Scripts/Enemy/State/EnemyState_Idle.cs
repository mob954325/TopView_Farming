using UnityEngine;

public class EnemyState_Idle : StateBase
{
    public float timer = 0;
    public float maxTimer = 1f;

    public override void OnEnterState()
    {
        // speed;
        Enemy.Controller.SetStop(false);
        Enemy.Controller.Speed = 0.8f;
    }

    public override void OnExcuting()
    {
        bool isStop = Enemy.Controller.CheckIsStop();

        if(Enemy.CheckPlayerInAttackArea())
        {
            Enemy.State = EnemyState.Attack;
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
                Enemy.Controller.SetStop(false);
            }

            if (isStop)
            {
                // 다음 이동 위치 선정
                Vector3 nextVec = SetRandomNextDestination();
                Enemy.Controller.SetDestination(nextVec);
                Enemy.Controller.SetStop(true);
            }
        }        
    }

    public override void OnExitState()
    {
        // endLookAround
        Enemy.Controller.Speed = 0f;
        timer = 0f;
        Enemy.Controller.SetStop(true);
    }

    private Vector3 SetRandomNextDestination()
    {
        Vector3 nextPosition = Random.onUnitSphere * 2f;
        return nextPosition;
    }
}
