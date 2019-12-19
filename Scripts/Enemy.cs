using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    private GameObject impactParticle;
    [SerializeField]
    private int maxHealth = 3;

    private int currentHealth;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(Character hitBy)
    {
        currentHealth--;
        Instantiate(impactParticle, new Vector3(0, 2, 0), Quaternion.identity);

        if (currentHealth <= 0)
            Die();
        else
            animator.SetTrigger("Hit");
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }
}
