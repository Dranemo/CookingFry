using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drawer : Outlining
{
    protected override void Interact(InputAction.CallbackContext context)
    {
        if(isOutlined)
            Debug.Log("Drawer Interact: " + name);
    }
}
