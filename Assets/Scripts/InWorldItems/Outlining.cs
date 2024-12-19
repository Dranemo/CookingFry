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

    protected void Awake()
    {
        interact.action.performed += Interact;
        interact.action.Enable();
    }
    private void OnDisable()
    {
        //interact.action.performed -= Interact;
        //interact.action.Disable();
    }   




    public virtual void SetOutlineBool(bool value)
    {
        if (value == isOutlined || outline == null)
        {
            return;
        }

        isOutlined = value;
        outline.enabled = value;
    }





    protected virtual void Interact(InputAction.CallbackContext context)
    {

    }



    public void IsInteractButtonEnabled(bool _bool)
    {
        if(_bool)
            interact.action.Enable();
        else
            interact.action.Disable();
    }

}
