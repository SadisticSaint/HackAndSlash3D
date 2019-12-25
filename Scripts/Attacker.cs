using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    [SerializeField]
    private int damage = 1;

    private float attackTimer;
    private float attackRefreshSpeed = 1.5f;

    public int Damage { get { return damage; } }

    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    public void Attack(ITakeDamage target)
    {
        attackTimer = 0;
        target.TakeDamage(this);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }
}