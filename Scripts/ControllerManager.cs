using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    //get all controllers and assign indices
    //assign controller

    private List<Controller> controllers; //Can this be a queue?

    private void Awake()
    {
        controllers = FindObjectsOfType<Controller>().ToList();

        int index = 1;

        foreach (var controller in controllers)
        {
            controller.SetIndex(index);
            index++;
        }
    }

    private void Update()
    {
        foreach (var controller in controllers)
        {
            if(controller.IsAssigned == false && controller.AnyButtonDown())
            {
                AssignController(controller);
            }
        }
    }

    private void AssignController(Controller controller)
    {
        controller.IsAssigned = true;
        FindObjectOfType<PlayerManager>().AddPlayerToGame(controller); //turn into singleton
    }
}
