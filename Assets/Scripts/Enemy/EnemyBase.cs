using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 적 행동 타입
/// </summary>
public enum EnemyState
{
    BeforeInitialize = 0,
    Idle,
    Attack,
    Dead
}

[RequireComponent(typeof(EnemyController))]
public class EnemyBase : MonoBehaviour, IHealth
{
    private Player target;

    /// <summary>
    /// 공격 목표 접근용 프로퍼티 (플레이어)
    /// </summary>
    public Player Target { get => target; }

    private EnemyController controller;

    /// <summary>
    /// EnemyController 접근 프로퍼티
    /// </summary>
    public EnemyController Controller { get => controller; }

    [SerializeField] private List<StateBase> states;
    private StateBase currentState;
    [SerializeField] private EnemyState state;

    protected EnemyState State
    {
        get => state;
        set
        {
            if(state != value) // 상태 변경됨
            {
                states[(int)state].OnExitState();  // 현재 상태 종료 진입

                state = value;
                states[(int)state].OnEnterState(); // 변경된 상태 진입
                currentState = states[(int)state]; // 상태 변경 내용
            }
        }
    }

    private float health = 0;
    private float maxHealth = 3;

    public float Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);

            if (health <= 0)
            {
                // 적 사망
                Debug.Log($"{gameObject.name}이(가) 사망했습니다");
                Dead();
            }
        }
    }
    public float MaxHealth { get; set; }
    public Action OnHit { get; set; }
    public Action OnDead { get; set; }

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    private void OnEnable()
    {
        target = FindAnyObjectByType<Player>();
        State = EnemyState.BeforeInitialize;

        MaxHealth = maxHealth;
        Health = MaxHealth;
    }

    protected virtual void Start()
    {
        // state Start
        int stateCount = Enum.GetValues(typeof(EnemyState)).Length;
        states = new List<StateBase>(stateCount);

        Transform child = transform.GetChild(0);
        states = child.GetComponents<StateBase>().ToList();

        State = EnemyState.Idle;
    }

    private void Update()
    {
        // executing State
        if(currentState != null)
        {
            currentState.OnExcuting();
        }
    }

    public void Hit(float damage)
    {
        Health -= damage;
        OnHit?.Invoke();
    }

    public void Dead()
    {
        State = EnemyState.Dead;
        OnDead?.Invoke();
    }
}