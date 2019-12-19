using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamage
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(Character hitBy)
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }
}
