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
        if (value == isOutlined || isCamera)
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
            StartCoroutine(WatchingTable(true));
        }
        else if (isCamera)
        {
            StartCoroutine(WatchingTable(false));
        }
    }

    IEnumerator WatchingTable(bool _bool)
    {
        virtualCamera.SetActive(_bool);
        if (!_bool)
        {
            yield return new WaitForSeconds(0.5f);
        }

        isCamera = _bool;

        if (_bool)
        {
            isOutlined = false;
            outline.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
