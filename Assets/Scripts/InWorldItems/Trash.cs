using UnityEngine.InputSystem;

public class Thrash : ItemShowingInvMenuInWorld
{
    private void Awake()
    {
        base.Awake();

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
            SetOutlineBool(false);
            return;
        }


        if (isOutlined)
        {

            forceOutline = true;
        }
    }



}
