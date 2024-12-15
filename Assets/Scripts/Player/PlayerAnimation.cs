using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private int HashToOnMove = Animator.StringToHash("IsMove");
    private int HashToOnAttack = Animator.StringToHash("OnAttack");

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
}