using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropItem : ItemShowingInvMenuInWorld
{
    static public DropItem instance;
    Vector3 pos;

    new private void Awake()
    {
        base.Awake();

        instance = this;
        showEveryHandOption = false;
        addItem = false;
    }

    protected override void Interact(InputAction.CallbackContext context)
    {
        if (IsPointerOverUIElement())
        {
            return;
        }
        if (forceOutline && !isOutlined)
        {
            canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(null);
            canvaInvItem.SetActive(false);

            forceOutline = false;
            SetHit(false, Vector3.zero);
            return;
        }


        if (isOutlined)
        {
            SetPos();
            forceOutline = true;
        }
    }


    public void SetHit(bool _hit, Vector3 _pos)
    {
        SetOutlineBool(_hit);
        pos = _pos;
    }


    void SetPos()
    {
        transform.position = pos + Vector3.up * 0.3f;

        canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(this, showEveryHandOption, addItem);
    }
}
