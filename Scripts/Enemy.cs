using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ITakeDamage
{
    //would it be better to make a singleton? would i be able to automatically attach this script to any enemy prefab running different instances of the script?
    [SerializeField]
    private GameObject impactParticle;
    [SerializeField]
    private int maxHealth = 3;

    private int currentHealth;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Character target;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(target == null)
        {
            target = Character.All
                .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
                .FirstOrDefault();
        }
        else
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
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
