using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyAttackArea : MonoBehaviour
{
    private EnemyBase enemyBase;
    private Collider attackCollider;

    private void Awake()
    {
        enemyBase = GetComponentInParent<EnemyBase>();
        attackCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        // 들어오면 공격
        if(other.gameObject.tag == "Player")
        {
            IHealth target = other.gameObject.GetComponent<Player>() as IHealth;
            enemyBase.Attack(target);
            attackCollider.enabled = false;
        }
    }

    public void SetColliderActive(bool value)
    {
        attackCollider.enabled = value;
    }
}
