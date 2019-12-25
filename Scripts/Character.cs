using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ITakeDamage, IAttack
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float attackOffset = 1f;
    [SerializeField]
    private float attackRadius = 1f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int maxHealth = 10;

    private Controller controller;
    private Animator animator;
    private Collider[] attackResults;
    private int currentHealth;

    public int Damage { get { return damage; } }

    public static List<Character> All = new List<Character>();

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attackResults = new Collider[10];

        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
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
            animator.SetTrigger("Attack"); //make character stop moving when attacking or create a new attack while moving animation
        }
    }

    /// <summary>
    /// Called by animation event via AnimationImpactWatcher
    /// </summary>
    private void AnimationImpactWatcher_OnImpact()
    {
        Vector3 position = transform.position + transform.forward * attackOffset;
        int hitCount = Physics.OverlapSphereNonAlloc(position, attackRadius, attackResults);

        for (int i = 0; i < hitCount; i++)
        {
            var takeDamage = attackResults[i].GetComponent<ITakeDamage>();
            if (takeDamage != null)
                takeDamage.TakeDamage(this);
        }
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }

    public void TakeDamage(IAttack hitBy)
    {
        currentHealth -= hitBy.Damage; //characters shouldn't be able to damage each other
    }
}
