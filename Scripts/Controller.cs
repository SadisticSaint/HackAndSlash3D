using System.Collections;
using System.Collections.Generic;
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

    private string attackButton;
    [SerializeField] //testing
    private bool attack;

    private void Start()
    {
        Index = 1;
        attackButton = "Attack" + Index;
    }

    private void Update()
    {
        attack = Input.GetButton(attackButton);
    }
}
