using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemShowingInvMenuInWorld : Outlining
{

    protected bool forceOutline = false;
    [SerializeField] protected GameObject canvaInvItem;








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
        if (forceOutline && outline.enabled)
        {
            if (canvaInvItem.activeSelf == false)
            {
                canvaInvItem.SetActive(true);
                canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(this);
            }
        }

        if(!forceOutline && !isOutlined && outline.enabled)
            outline.enabled = false;
    }

    public void SetForceOutline(bool value)
    {
        forceOutline = value;
    }
}
