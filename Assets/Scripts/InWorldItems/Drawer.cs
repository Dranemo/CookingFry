using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drawer : Outlining
{
    [SerializeField] string name;
    [SerializeField] List<DrawerFloor> floors;
    [SerializeField] GameObject canvasDrawer;
    protected override void Interact(InputAction.CallbackContext context)
    {
        if (isOutlined)
        {
            canvasDrawer.GetComponentInChildren<CanvasDrawers>().SetDrawer(name, floors);
            canvasDrawer.SetActive(true);
        }
    }
}
