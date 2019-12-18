﻿using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private Controller controller;

    private void Update()
    {
        Vector3 direction = controller.GetDirection();

        if (direction.magnitude > 0.1) //make dead zone menu setting
        {
            transform.position += direction * Time.deltaTime * moveSpeed;
        }
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }
}
