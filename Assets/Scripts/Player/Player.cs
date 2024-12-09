using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]
public class Player : MonoBehaviour, IHealth, ICombatable
{
    PlayerController controller;
    PlayerInput input;

    Material material_Body;

    public float health = 0;
    private float maxHealth = 10;
    private bool isImmunite = false;

    public float Health 
    { 
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, MaxHealth);

            isImmunite = true;
            StartCoroutine(HitProcess());

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
    }

    private void OnEnable()
    {
        MaxHealth = 10;
        Health = MaxHealth;
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
            OnHit?.Invoke();
        }
    }
    
    private IEnumerator HitProcess()
    {
        material_Body.color = Color.red;

        float timeElapsed = 0.0f;
        float maxTime = 0.5f;
        float timeRatio = 1f / maxTime;

        while(timeElapsed < 0.5f)
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
        target.Hit(AttackPower);
        onAttack?.Invoke();
    }

    public void Defence()
    {
        onDefence?.Invoke();
    }
}