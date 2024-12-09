using Unity.VisualScripting;
using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    private EnemyBase enemy;

    /// <summary>
    /// EnemyBase 접근 프로퍼티
    /// </summary>
    protected EnemyBase Enemy { get => enemy; }        

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyBase>();
    }

    /// <summary>
    /// 상태 진입 시 한 번 실행되는 함수
    /// </summary>
    public abstract void OnEnterState();

    /// <summary>
    /// 상태 진입 후 계속 실행될 함수 ( Update ) 
    /// </summary>
    public abstract void OnExcuting();

    /// <summary>
    /// 상태 탈출 시 한 번 실행되는 함수
    /// </summary>
    public abstract void OnExitState();
}