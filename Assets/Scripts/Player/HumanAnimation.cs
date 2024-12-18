using UnityEngine;

public class HumanAnimation : MonoBehaviour
{
    private Animator anim;

    private int HashToOnMove = Animator.StringToHash("IsMove");
    private int HashToOnAttack = Animator.StringToHash("OnAttack");
    private int HashToOnDead = Animator.StringToHash("OnDead");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayMove(bool value)
    {
        anim.SetBool(HashToOnMove, value);
    }

    public void PlayAttack()
    {
        anim.SetTrigger(HashToOnAttack);
    }

    public void PlayDead()
    {
        anim.SetTrigger(HashToOnDead);
    }
}