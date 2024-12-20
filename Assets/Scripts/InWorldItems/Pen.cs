using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pen : Outlining
{
    [SerializeField] GameObject CanvaNewRecipe;
    [SerializeField] GameObject bookCanvas;
    [SerializeField] Outlining support;

    [SerializeField] GameObject pen;

    protected override void Interact(InputAction.CallbackContext context)
    {
        if (isOutlined)
        {
            bookCanvas.GetComponent<GraphicRaycaster>().enabled = false;
            support.IsInteractButtonEnabled(false);
            CanvaNewRecipe.GetComponent<RecipeCreationCanvas>().SetSupport(support);

            CanvaNewRecipe.SetActive(true);
            SetOutlineBool(false);

            foreach (Transform child in pen.transform)
            {
                    child.GetComponent<MeshCollider>().enabled = false;
            }
        }
    }
}
