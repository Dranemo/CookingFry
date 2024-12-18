using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropItem : ItemShowingInvMenuInWorld
{
    static public DropItem instance;

    bool hit = false;
    Vector3 pos = Vector3.zero;

    private void Awake()
    {
        instance = this;
    }


    protected override void Interact(InputAction.CallbackContext context)
    {
        if (IsPointerOverUIElement())
        {
            return;
        }


        if (hit)
        {
            showCanvas();
        }
        else
        {
            canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(null);
            canvaInvItem.SetActive(false);
        }
    }


    public void SetHit(bool _hit, Vector3 _pos)
    {
        hit = _hit;
        pos = _pos;
    }




    public void showCanvas()
    {
        transform.position = pos;

        canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(this, false);
        canvaInvItem.SetActive(true);
    }
}
