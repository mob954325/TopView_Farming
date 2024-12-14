using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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
public class EnemyBase : Product, IHealth, ICombatable, IInteractable
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

    public EnemyDataSO data;
    private Material material_Body;

    private List<StateBase> states;

    private Inventory inventory;
    private InventoryUI inventoryUI;
    private ContextMenuUI contextMenu;
    public ContextType contextType;

    private StateBase currentState;
    private EnemyState state;

    public EnemyState State
    {
        get => state;
        set
        {
            if(state != EnemyState.BeforeInitialize) states[(int)state].OnExitState();  // 현재 상태 종료 진입

            state = value;
            states[(int)state].OnEnterState(); // 변경된 상태 진입
            currentState = states[(int)state]; // 현재 상태 변경
        }
    }

    [SerializeField] private float health = 0;
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
                CanInteract = true;
                Dead();
            }
        }
    }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public Action OnHit { get; set; }
    public Action OnDead { get; set; }

    private float attackRatePerSec = 1f;
    private float attackPower = 1f;
    private float defencePower = 0f;

    public float AttackRatePerSec { get => attackRatePerSec; set => attackRatePerSec = value; }
    public float AttackPower { get => attackPower; set => attackPower = value; }
    public float DefencePower { get => defencePower; set => defencePower = value; }
    public Action onAttack { get; set; }
    public Action onDefence { get; set; }

    private bool canInteract = false;
    public bool CanInteract { get => canInteract; set => canInteract = value; }

    // Unity ===================================================
    private void Awake()
    {
        material_Body = transform.GetChild(1).GetComponent<MeshRenderer>().material;
        inventoryUI = FindFirstObjectByType<InventoryUI>();
        contextMenu = FindAnyObjectByType<ContextMenuUI>();

        // controller 초기화
        controller = GetComponent<EnemyController>();
        controller.Init();

        target = FindAnyObjectByType<Player>();

        // 상태 스크립트 초기화
        int stateCount = Enum.GetValues(typeof(EnemyState)).Length;
        states = new List<StateBase>(stateCount);

        Transform child = transform.GetChild(0);
        states = child.GetComponents<StateBase>().ToList();

        for(int i = 0; i < states.Count; i++)
        {
            states[i].Init();
        }
    } 

    private void OnEnable()
    {
        State = EnemyState.BeforeInitialize;

        MaxHealth = maxHealth;
        Health = MaxHealth;

        inventory = new Inventory(inventoryUI);
        SetDropItem();
        inventory.OnDiscardItem += () =>
        {
            if (inventory.CheckIsInventoryEmpty())
            {
                inventory.InventoryUI.SetDeactive();
                this.gameObject.SetActive(false);
            }
        };

        CanInteract = false;
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

    // IHealth ===================================================
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

    // ICombatable ===================================================
    public void Attack(IHealth target)
    {
        target.Hit(AttackPower);
    }

    private IEnumerator HitProcess()
    {
        // 피격 이펙트 (머터리얼이 빨강으로 변하고 천천히 원래 색으로 돌아옴)
        material_Body.color = Color.red;

        float timeElapsed = 0.0f;
        float maxTime = 0.5f;
        float timeRatio = 1f / maxTime;
        while (timeElapsed < 0.5f)
        {
            timeElapsed += Time.deltaTime;
            material_Body.color = new Color
                (1f,
                timeElapsed * timeRatio,
                timeElapsed * timeRatio);

            yield return null;
        }
    }

    public void Defence()
    {
        // 방어
    }

    // 아이템 ==========================================================

    /// <summary>
    /// 적이 생성할 드랍템 추가
    /// </summary>
    private void SetDropItem()
    {
        int count = data.dataTable.Count;
        List<ItemDataSO> dropItems = new List<ItemDataSO>(count);

        for(int i = 0; i < count; i++)
        {
            float rand = UnityEngine.Random.Range(0f, 1f);
            if(rand <= data.dataTable[i].dropRate)
            {
                inventory.AddItem(data.dataTable[i].itemData);
            }
        }
    }

    // 기타 ============================================================

    private float attackRange = 5f;

    /// <summary>
    /// 공격 범위 내에 있으면 true 아니면 false
    /// </summary>
    public bool CheckPlayerInAttackArea()
    {
        bool result = false;

        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in detectedColliders) 
        {
            if (collider.gameObject.tag == "Player")
            {                
                result = true;
            }
        }

        return result;
    }

    private float sightAngle = 15f;

    /// <summary>
    /// 플레이어가 시야 범위 내에 있으면 true 아니면 false (+-sightAngle 범위)
    /// </summary>
    /// <returns></returns>
    public bool CheckPlayerInSight()
    {
        bool result = false;

        if (target != null)
        {
            Vector3 lookVec = target.transform.position - transform.position;
            float sqrDistance = lookVec.sqrMagnitude;

            float angle = Vector3.Angle(transform.forward, target.transform.position);
            result = true;
        }

        return result;
    }

    public void OnInteract()
    {
        if(canInteract)
        {
            contextMenu.OnActive(contextType, inventory, Mouse.current.position.value);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = CheckPlayerInAttackArea() ? Color.red : Color.green;
        Handles.DrawWireDisc(transform.position, transform.up, attackRange, 2f);

        Quaternion q1 = Quaternion.AngleAxis(sightAngle, Vector3.up);
        Quaternion q2 = Quaternion.AngleAxis(-sightAngle, Vector3.up);

        Handles.color = CheckPlayerInSight() ? Color.red : Color.yellow;
        Handles.DrawLine(transform.position, transform.position + q1 * transform.forward * attackRange, 2f);
        Handles.DrawLine(transform.position, transform.position + q2 * transform.forward * attackRange, 2f);
    }
#endif
}