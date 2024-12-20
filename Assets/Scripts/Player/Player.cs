using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController), typeof(PlayerInput), typeof(HumanAnimation))]
public class Player : MonoBehaviour, IHealth, ICombatable
{
    private SoundManager soundManager;
    private PlayerController controller;
    private PlayerInput input;
    /// <summary>
    /// 플레이어 인풋 접근용 프로퍼티
    /// </summary>
    public PlayerInput Input { get => input; }
    private HumanAnimation anim;
    public HealthBar playerHealthBar;

    private Material material_Body;
    private WeaponSlot weaponSlot;
    private ItemDataSO_Equipable weaponData;
    public ItemDataSO_Equipable WeaponData { get => weaponData; }

    private Inventory inventory;
    public Inventory Inventory { get => inventory; }

    public InventoryUI inventoryUI;

    public GameObject worldObject => this.gameObject;

    private float health = 0;
    private float maxHealth = 10;
    private bool isImmunite = false;

    public float Health 
    { 
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, MaxHealth);            

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

    private bool isFootStepPlay = false;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        input = GetComponent<PlayerInput>();
        anim = GetComponent<HumanAnimation>();
        material_Body = transform.GetChild(1).GetComponent<MeshRenderer>().material;
        weaponSlot = FindAnyObjectByType<WeaponSlot>();
        soundManager = GetComponentInChildren<SoundManager>();
    }

    private void OnEnable()
    {
        MaxHealth = 10;
        Health = MaxHealth;
        isImmunite = false;

        if(playerHealthBar != null) playerHealthBar.Init(this);
    }

    private void Start()
    {
        if(inventoryUI != null)
        {
            inventory = new Inventory(inventoryUI);
        }

        Input.OnInvenOpen += () => { inventoryUI.ToggleActive(Inventory, ContextType.InventorySlot); }; // 인벤토리 열기
        controller.OnMoveAction += () =>
        {
            // 애니메이션
            bool isMove = input.MoveVec.sqrMagnitude > 0;
            anim.PlayMove(isMove);
            
            if(isMove && !isFootStepPlay)
            {
                soundManager.PlaySound(SoundType.footStep);
                isFootStepPlay = true;
            }
            else if(!isMove)
            {
                soundManager.StopSound(SoundType.footStep);
                isFootStepPlay = false;
            }
        };

        input.OnAttack += () =>
        {
            anim.PlayAttack();
        };
    }

    private void Update()
    {
        controller.OnMove(input.MoveVec);
    }

    // IHealth ===============================================================
    public void Dead()
    {
        anim.PlayDead();
        OnDead?.Invoke();
    }

    public void Hit(float damage)
    {
        if (Health <= 0) return;

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

        if(target.Health > 0)
        {
            soundManager.PlaySound(SoundType.Hit);
        }
        input.OnAttack?.Invoke();
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