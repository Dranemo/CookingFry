using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Thrash : ItemShowingInvMenuInWorld
{

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
            SetOutlineBool(false);
            return;
        }


        if (isOutlined)
        {
            forceOutline = true;
        }
    }





}
