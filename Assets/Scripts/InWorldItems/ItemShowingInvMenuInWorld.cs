using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ItemShowingInvMenuInWorld : Outlining
{

    protected bool forceOutline = false;
    protected bool showEveryHandOption = true;
    protected bool addItem = true;

    protected GameObject canvaInvItem;



    [SerializeField] private InputActionReference tableInput;



    protected void Start()
    {
        canvaInvItem = CanvaInvItemScript.instance.gameObject;

    }

    new protected void Awake()
    {
        base.Awake();


        tableInput.action.performed += CloseEverything;

        return;

    }





    protected bool IsPointerOverUIElement()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }


    protected void Update()
    {


        if (isOutlined || forceOutline)
        {
            if (outline.enabled == false)
            {
                    outline.enabled = true;
            }

            if (forceOutline && outline.enabled)
            {
                if (canvaInvItem.activeSelf == false && PlayerSingleton.playerStats.WhatWatchingTable() != null)
                {


                    canvaInvItem.SetActive(true);
                    canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(this, showEveryHandOption, addItem);
                }
            }
            return;
        }


        

        if(!forceOutline && outline.enabled)
            outline.enabled = false;
    }

    public void SetForceOutline(bool value)
    {
        forceOutline = value;
    }



    void CloseEverything(InputAction.CallbackContext context)
    {
            canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(null);
            canvaInvItem.SetActive(false);

            forceOutline = false;
            SetOutlineBool(false);
            return;
    }
}
