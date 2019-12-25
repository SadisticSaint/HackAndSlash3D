using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float attackOffset = 1f;
    [SerializeField]
    private float attackRadius = 1f;

    private float attackTimer;
    private float attackRefreshSpeed = 1.5f;
    private Collider[] attackResults;

    public int Damage { get { return damage; } }

    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    private void Awake()
    {
        attackResults = new Collider[10];

        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        if (animationImpactWatcher != null)
            animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
    }

    public void Attack(ITakeDamage target)
    {
        attackTimer = 0;
        target.TakeDamage(this);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
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
}