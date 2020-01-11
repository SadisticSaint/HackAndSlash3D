using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : PooledMonoBehaviour, ITakeDamage
{
    //would it be better to make a singleton? would i be able to automatically attach this script to any enemy prefab running different instances of the script?
    [SerializeField]
    private PooledMonoBehaviour impactParticle;
    [SerializeField]
    private int maxHealth = 3;

    private int currentHealth;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Attacker attacker;
    private Character target;

    private bool IsDead { get { return currentHealth <= 0; } }


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (IsDead)
            return;

        if (target == null)
        {
            AcquireTarget();
        }
        else
        {
            var distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > 2)
            {
                FollowTarget();
            }
            else
            {
                TryToAttack();
            }
        }
    }

    private void AcquireTarget()
    {
        target = Character.All
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
        animator.SetFloat("Speed", 0f);
    }

    private void FollowTarget()
    {
        animator.SetFloat("Speed", 1f);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void TryToAttack()
    {
        animator.SetFloat("Speed", 0f);
        navMeshAgent.isStopped = true;

        if (attacker.CanAttack)
        {
            animator.SetTrigger("Attack");
            attacker.Attack(target);
        }
    }

    public void TakeDamage(IAttack hitBy)
    {
        currentHealth--;
        //*stop impact particles after death
        //refine how many particles should exist at once in particle prefab
        impactParticle.Get<PooledMonoBehaviour>(transform.position + new Vector3(0, 2, 0), Quaternion.identity);

        if (IsDead)
            Die();
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Die");

        ReturnToPool(6f);
    }
}
