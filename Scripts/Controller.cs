using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //need a way to reference different buttons for different moves
    //need to know what controller number it is
    //need to know the input strings

    /// <summary>
    /// Button 0 - X
    /// Button 1 - A
    /// Button 2 - B
    /// Button 3 - Y
    /// </summary>

    public int Index { get; private set; }
    public bool IsAssigned { get; set; }

    [SerializeField] //testing
    private bool attack;
    private string attackButton;

    private void Update()
    {
        if(!string.IsNullOrEmpty(attackButton)) //fix for - ArgumentException: Input button _ is not set up
            attack = Input.GetButton(attackButton);
    }

    internal void SetIndex(int index)
    {
        Index = index;
        attackButton = "Attack" + Index;
        gameObject.name = "Controller " + Index;
    }

    internal bool AnyButtonDown()
    {
        return attack; //as more buttons are added, this needs to be modified
    }
}
