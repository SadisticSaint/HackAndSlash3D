using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class Character : MonoBehaviour, ITakeDamage
{
    public static List<Character> All = new List<Character>();

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int maxHealth = 10;

    private Controller controller;
    private Animator animator;
    private Attacker attacker;
    private int currentHealth;

    public event Action<int, int> OnHealthChanged = delegate { };
    public event Action<Character> OnDied = delegate { };

    public int Damage { get { return damage; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attacker = GetComponent<Attacker>();
    }
    private void OnEnable()
    {
        if (All.Contains(this) == false)
            All.Add(this);

        currentHealth = maxHealth;
    }

    private void OnDisable()
    {
        if (All.Contains(this))
            All.Remove(this);
    }

    private void Update()
    {
        Vector3 direction = controller.GetDirection();

        if (direction.magnitude > 0.1) //make dead zone menu setting
        {
            transform.position += direction * Time.deltaTime * moveSpeed;
            transform.forward = direction * 360; //*stop character from trying to correct its rotation

            animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (controller.attackPressed)
        {
            if (attacker.CanAttack)
            {
                animator.SetTrigger("Attack"); //make character stop moving when attacking or create a new attack while moving animation

            }
        }
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }

    public void TakeDamage(IAttack hitBy)
    {
        currentHealth -= hitBy.Damage; //characters shouldn't be able to damage each other
        OnHealthChanged(currentHealth, maxHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDied(this);
    }
}
