using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(PlayerInput))]
public class Player : MonoBehaviour, IHealth
{
    PlayerController controller;
    PlayerInput input;

    private float health = 0;
    private float maxHealth = 10;

    public float Health 
    { 
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);

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

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        input = GetComponent<PlayerInput>();
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

    public void Dead()
    {
        OnDead?.Invoke();
    }

    public void Hit(float damage)
    {
        Health -= damage;
        OnHit?.Invoke();
    }
}