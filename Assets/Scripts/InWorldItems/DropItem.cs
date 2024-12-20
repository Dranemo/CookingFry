using UnityEngine;
using UnityEngine.InputSystem;

public class DropItem : ItemShowingInvMenuInWorld
{
    static public DropItem instance;
    Vector3 pos;

    new protected void Awake()
    {
        instance = this;
        showEveryHandOption = false;
        addItem = false;

        base.Awake();
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
        transform.position = pos;

        canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(this, showEveryHandOption, addItem);
    }
}
