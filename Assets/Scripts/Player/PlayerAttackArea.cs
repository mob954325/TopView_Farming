using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerAttackArea : MonoBehaviour
{
    private Player player;
    private Collider attackCollider;

    private WaitForSeconds colliderDisableDelay = new WaitForSeconds(0.2f);

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        attackCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        attackCollider.enabled = false;
    }

    private void Start()
    {
        player.Input.OnAttack += (() => { StartCoroutine(AttackProcess()); });
    }

    private void OnTriggerStay(Collider other)
    {
        // 들어오면 공격
        Debug.Log(other);
        if (other.gameObject.tag == "Enemy")
        {
            IHealth target = other.gameObject.GetComponent<EnemyBase>() as IHealth;
            player.Attack(target);
            attackCollider.enabled = false;
        }
    }

    private IEnumerator AttackProcess()
    {
        attackCollider.enabled = true;
        yield return colliderDisableDelay;
        attackCollider.enabled = false;

    }
}
