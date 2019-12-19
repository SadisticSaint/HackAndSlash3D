using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float attackOffset = 1f;
    [SerializeField]
    private float attackRadius = 1f;

    private Controller controller;
    private Animator animator;
    private Collider[] attackResults;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attackResults = new Collider[10];

        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
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
            var box = attackResults[i].GetComponent<Box>();
            if (box != null)
                box.TakeDamage(this);
        }
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }
}
