using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    private float forceAmount = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(Character hitBy)
    {
        var direction = Vector3.Normalize(transform.position - hitBy.transform.position);

        rb.AddForce(direction * forceAmount, ForceMode.Impulse);
    }
}
