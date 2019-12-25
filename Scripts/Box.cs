using UnityEngine;

public class Box : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    private float forceAmount = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(IAttack hitBy) //should enemies be able to interact with all objects that players can?
    {
        var direction = Vector3.Normalize(transform.position - hitBy.transform.position);

        rb.AddForce(direction * forceAmount, ForceMode.Impulse);
    }
}
