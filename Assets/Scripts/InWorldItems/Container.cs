using UnityEngine;
using UnityEngine.InputSystem;

public class Container : ItemShowingInvMenuInWorld
{
    [SerializeField] Oven oven;
    Food Food;


    new private void Awake()
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



    public void AddItem(Food _food)
    {
        if (_food == null)
        {
            Food = null;
            return;
        }


        if (!_food.CanBeCooked())
            return;

        Food = _food;
        Food.container = this;
    }

    public bool CanAddItem()
    {
        return Food == null;
    }


    new private void Update()
    {
        base.Update();

        if(Food != null)
        {
            if (Food.CanBeCooked())
            {
                float cookTime = oven.ReturnHotValue() * Time.deltaTime;
                Food.Cooking(cookTime);
            }
        }
    }
}
