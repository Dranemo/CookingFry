using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pen : Outlining
{
    [SerializeField] GameObject CanvaNewRecipe;

    protected override void Interact(InputAction.CallbackContext context)
    {
        if(isOutlined)
        {
            Debug.Log("Pen Interact");
        }
    }
}
