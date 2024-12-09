using System;
using UnityEngine;

public interface IHealth
{
    /// <summary>
    /// 현재 체력
    /// </summary>
    float Health { get; set; }

    float MaxHealth { get; set; }

    /// <summary>
    /// 피격 시 실행되는 델리게이트
    /// </summary>
    Action OnHit { get; set; }

    /// <summary>
    /// 사망 시 실행되는 델리게이트
    /// </summary>
    Action OnDead { get; set; }

    /// <summary>
    /// 피격 시 실행되는 함수
    /// </summary>
    /// <param name="damage">공격 데미지</param>
    public void Hit(float damage);

    /// <summary>
    /// 사망 시 실행되는 함수
    /// </summary>
    public void Dead();
}
