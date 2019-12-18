using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private Controller controller;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }
}
