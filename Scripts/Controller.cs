using UnityEngine;

public class Controller : MonoBehaviour
{
    /// <summary>
    /// Button 0 - X
    /// Button 1 - A
    /// Button 2 - B
    /// Button 3 - Y
    /// </summary>

    [SerializeField] //* delete after testing
    private bool attack;
    public bool attackPressed; //*make private after testing
    private string attackButton;
    private string horizontalAxis;
    private string verticalAxis;
    public float horizontal; //*make property
    public float vertical; //*make property

    public int Index { get; private set; }
    public bool IsAssigned { get; set; }

    private void Update()
    {
        if (!string.IsNullOrEmpty(attackButton)) //fix for - ArgumentException: Input button _ is not set up
        {
            attack = Input.GetButton(attackButton);
            attackPressed = Input.GetButtonDown(attackButton);
            horizontal = Input.GetAxis(horizontalAxis);
            vertical = Input.GetAxis(verticalAxis);
        }
    }

    internal void SetIndex(int index)
    {
        Index = index;
        attackButton = "Attack" + Index;
        horizontalAxis = "Horizontal" + Index;
        verticalAxis = "Vertical" + Index;
        gameObject.name = "Controller " + Index;
    }

    internal bool AnyButtonDown()
    {
        return attack; //*as more buttons are added, this needs to be modified
    }
}
