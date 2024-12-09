using System;
using UnityEngine;

public interface ICombatable
{
    public float AttackRatePerSec { get; set; }

    public float AttackPower { get; set; }

    public float DefencePower { get; set; }

    /// <summary>
    /// 공격 시 실행되는 델리게이트
    /// </summary>
    public Action onAttack { get; set; }

    /// <summary>
    /// 방어 시 실행되는 델리게이트
    /// </summary>
    public Action onDefence { get; set; }

    /// <summary>
    /// 공격 시 실행되는 함수
    /// </summary>
    public void Attack(IHealth target);

    /// <summary>
    /// 방어 시 실행되는 함수
    /// </summary>
    public void Defence();
}