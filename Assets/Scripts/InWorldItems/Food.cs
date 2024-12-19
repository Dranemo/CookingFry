using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Food : ItemShowingInvMenuInWorld
{
    [SerializeField] float inHandOffset = 1.5f;
    [SerializeField] float upOffset = 0.5f;

    [SerializeField] KitchenElement kitchenElement;


    Vector3 defaultTransformPos;
    Quaternion defaultTransformRot;
    Vector3 defaultTransformScale;

    new private void Awake()
    {
        base.Awake();

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
        //transform.position = defaultTransformPos;
        transform.rotation = defaultTransformRot;
        transform.localScale = defaultTransformScale;
    }
    public void SetInHandOffset(Vector3 forward)
    {
        transform.position += forward * inHandOffset;
    }
    public void SetPosItemDrop(Vector3 pos)
    {
        ResetTransform();
        transform.position = pos + Vector3.up * upOffset;
    }

    public KitchenElement GetKitchenElement()
    {
        return kitchenElement;
    }



    public void SetDefaultTransform (Vector3 pos, Quaternion rot, Vector3 scale)
    {
        defaultTransformPos = pos;
        defaultTransformRot = rot;
        defaultTransformScale = scale;
    }

    public Quaternion GetDefaultRot()
    {
        return defaultTransformRot;
    }
    public Vector3 GetDefaultPos()
    {
        return defaultTransformPos;
    }
    public Vector3 GetDefaultScale()
    {
        return defaultTransformScale;
    }
}
