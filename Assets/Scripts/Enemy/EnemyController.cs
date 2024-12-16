using System;
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

    public Action OnMoveAction { get; set; }

    private void Update()
    {
        OnMoveAction?.Invoke();
    }

    public void Init()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void OnMove(Vector3 moveDir)
    {
        // 사용 안함  
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