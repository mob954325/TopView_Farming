using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IMoveable
{    
    private NavMeshAgent navAgent;

    private float speed = 1.0f;

    public float Speed 
    {
        get => speed;
        set
        {
            speed = value;
            navAgent.speed = value;
        }
    }


    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// navMeshAgent 사용한 움직임 함수
    /// </summary>
    /// <param name="moveDir">목적지</param>
    public void OnMove(Vector3 moveDir)
    {
        //navAgent.Move(moveDir);     
    }

    public void SetStop(bool value)
    {
        navAgent.isStopped = value;
    }

    public void SetDestination(Vector3 destinationVec)
    {
        navAgent.destination = destinationVec;
    }

    /// <summary>
    /// 멈추면 true 아니면 false
    /// </summary>
    public bool CheckIsStop()
    {
        return navAgent.remainingDistance < navAgent.stoppingDistance;
    }
}