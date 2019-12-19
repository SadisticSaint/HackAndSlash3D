using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamage
{
    public void TakeDamage(Character hitBy)
    {
        Destroy(gameObject);
    }
}
