using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Food : ItemShowingInvMenuInWorld
{
    [SerializeField] float inHandOffset = 1.5f;


    Vector3 defaultTransformPos;
    Quaternion defaultTransformRot;
    Vector3 defaultTransformScale;

    private void Start()
    {
        defaultTransformPos = transform.position;
        defaultTransformRot = transform.rotation;
        defaultTransformScale = transform.localScale;
    }


    protected override void Interact(InputAction.CallbackContext context)
    {
        if(IsPointerOverUIElement())
        {
            return;
        }



        if (forceOutline && !isOutlined)
        {
            canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(null);
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







    
    public void ResetTransform()
    {
        transform.position = defaultTransformPos;
        transform.rotation = defaultTransformRot;
        transform.localScale = defaultTransformScale;
    }
    public void SetInHandOffset(Vector3 forward)
    {
        transform.position += forward * inHandOffset;
    }
}
