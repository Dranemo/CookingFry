using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Table : MonoBehaviour
{
    [SerializeField] InputActionReference interact;
    [SerializeField] Outline outline;
    [SerializeField] GameObject virtualCamera;


    bool isOutlined = false;
    bool isCamera = false;

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




    public void SetOutlineBool(bool value)
    {
        if(value == isOutlined || isCamera)
        {
            return;
        }

        isOutlined = value;
        outline.enabled = value;
    }





    private void Interact(InputAction.CallbackContext context)
    {
        if (isOutlined && !isCamera)
        {
            virtualCamera.SetActive(true);
            isCamera = true;
            isOutlined = false;
            outline.enabled = false;
        }
        else if (isCamera)
        {
            virtualCamera.SetActive(false);
            isCamera = false;
        }
    }
}
