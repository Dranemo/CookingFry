using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Outlining : MonoBehaviour
{
    [SerializeField] protected InputActionReference interact;
    [SerializeField] protected Outline outline;


    protected bool isOutlined = false;

    private void OnEnable()
    {
        interact.action.performed += Interact;
        interact.action.Enable();
    }
    private void OnDisable()
    {
        interact.action.performed -= Interact;
        interact.action.Disable();
    }




    public virtual void SetOutlineBool(bool value)
    {
        if (value == isOutlined)
        {
            return;
        }

        isOutlined = value;
        outline.enabled = value;
    }





    protected virtual void Interact(InputAction.CallbackContext context)
    {

    }
    
}
