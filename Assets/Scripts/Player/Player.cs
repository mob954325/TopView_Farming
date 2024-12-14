using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]
public class Player : MonoBehaviour, IHealth, ICombatable
{
    private PlayerController controller;
    private PlayerInput input;

    /// <summary>
    /// 플레이어 인풋 접근용 프로퍼티
    /// </summary>
    public PlayerInput Input { get => input; }

    private Material material_Body;
    private WeaponSlot weaponSlot;
    private ItemDataSO_Equipable weaponData;

    private Inventory inventory;
    public Inventory Inventory { get => inventory; }

    public InventoryUI inventoryUI;

    private float health = 0;
    private float maxHealth = 10;
    private bool isImmunite = false;

    public float Health 
    { 
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, MaxHealth);            

            Debug.Log($"플레이어 체력 : {health}");
            if ( health <= 0 )
            {
                // 플레이어 사망
                Debug.Log("플레이어가 사망했습니다");
                Dead();
            }
        }
    }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public Action OnHit { get; set; }
    public Action OnDead { get; set; }

    private float attackRatePerSec = 1f;
    private float attackPower = 1f;
    private float defencePower = 1f;

    public float AttackRatePerSec { get => attackRatePerSec; set => attackRatePerSec = value; }
    public float AttackPower { get => attackPower; set => attackPower = value; }
    public float DefencePower { get => defencePower; set => defencePower = value; }
    public Action onAttack { get; set; }
    public Action onDefence { get; set; }

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        input = GetComponent<PlayerInput>();
        material_Body = transform.GetChild(1).GetComponent<MeshRenderer>().material;
        weaponSlot = FindAnyObjectByType<WeaponSlot>();
    }

    private void OnEnable()
    {
        MaxHealth = 10;
        Health = MaxHealth;
        isImmunite = false;
    }

    private void Start()
    {
        inventory = new Inventory(inventoryUI);

        Input.OnInvenOpen += () => { inventoryUI.ToggleActive(Inventory, ContextType.Inventory); }; // 인벤토리 열기
    }

    private void Update()
    {
        controller.OnMove(input.MoveVec);
    }

    // IHealth ===============================================================
    public void Dead()
    {
        OnDead?.Invoke();
    }

    public void Hit(float damage)
    {
        if(!isImmunite)
        {
            Health -= damage;
            StartCoroutine(HitProcess());
            OnHit?.Invoke();
        }
    }
    
    private IEnumerator HitProcess()
    {
        isImmunite = true;

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

        isImmunite = false;
    }

    // ICombatable ===============================================================

    public void Attack(IHealth target)
    {
        target.Hit(AttackPower + weaponSlot.WeaponDamage);
        onAttack?.Invoke();
    }

    public void Defence()
    {
        onDefence?.Invoke();
    }

    // 기타 ======================================================================

    public void EquipWeapon(ItemDataSO_Equipable data)
    {
        weaponSlot.AddWeapon(data);
        weaponData = data;
    }

    public void UnEquipWeapon()
    {
        weaponData = null;
        weaponSlot.RemoveWeapon();
    }
}