using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Food : Outlining
{
    [SerializeField] private GameObject canvaInvItem;
    bool forceOutline = false;

    [SerializeField] float inHandOffset = 1.5f;


    Vector3 defaultRotation;

    private void Start()
    {
        defaultRotation = transform.rotation.eulerAngles;
    }


    protected override void Interact(InputAction.CallbackContext context)
    {
        if(IsPointerOverUIElement())
        {
            return;
        }



        if (forceOutline && !isOutlined)
        {
            canvaInvItem.GetComponent<CanvaInvItemScript>().SetFood(null);
            canvaInvItem.SetActive(false);

            forceOutline = false;
            SetOutlineBool(false);
            return;
        }


        if (isOutlined)
        {

            forceOutline = true;
        }
    }

    private void Update()
    {
        if(forceOutline && !outline.enabled)
        {
            outline.enabled = true;
            if(canvaInvItem.activeSelf == false)
            {
                canvaInvItem.SetActive(true);
                canvaInvItem.GetComponent<CanvaInvItemScript>().SetFood(this);
            }
        }

        if(!forceOutline && outline.enabled)
            outline.enabled = false;
    }






    private bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }






    public void SetForceOutline(bool value)
    {
        forceOutline = value;
    }
    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(defaultRotation);
    }
    public void SetInHandOffset(Vector3 forward)
    {
        transform.position += forward * inHandOffset;
    }
}
