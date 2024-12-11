using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator = null;
    [SerializeField] float attackTime = 1.0f;
    [SerializeField] float hitTime = 1.0f;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        animator.Play("Idle");
    }

    public void PlayAttack()
    {
        animator.Play("Attack");
        Invoke(nameof(PlayIdle), attackTime);
    }

    public void PlayHit()
    {
        animator.Play("Hit");
        Invoke(nameof(PlayIdle), hitTime);
    }

    public void PlayDeath()
    {
        animator.Play("Death");
    }
}