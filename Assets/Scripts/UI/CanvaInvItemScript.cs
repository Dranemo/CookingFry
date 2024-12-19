using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaInvItemScript : MonoBehaviour
{
    [SerializeField] Button rightHandButton;
    [SerializeField] Button leftHandButton;

    ItemShowingInvMenuInWorld item = null;
    bool showBothButton = true;
    bool addItem = true;

    public static CanvaInvItemScript instance;






    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        rightHandButton.onClick.AddListener(() => InteractButton(1));
        leftHandButton.onClick.AddListener(() => InteractButton(0));

        instance = this;
        gameObject.SetActive(false);
    }

    public void SetItem(ItemShowingInvMenuInWorld _item, bool _showBothButton = true, bool _addItem = true)
    {
        item = _item;
        showBothButton = _showBothButton;
        addItem = _addItem;

        if (item != null)
        {
            SetPosition();
            SetButton();
        }
    }


    private void SetButton()
    {
        if (!showBothButton)
        {
            Inventory inv = PlayerSingleton.instance.GetComponent<Inventory>();
            if (inv.GetHand(0) == null && addItem)
            {
                leftHandButton.gameObject.SetActive(true);
            }else if (inv.GetHand(0) == null)
            {
                leftHandButton.gameObject.SetActive(false);
            }


            if (inv.GetHand(1) == null && addItem)
            {
                rightHandButton.gameObject.SetActive(true);
            }
            else if (inv.GetHand(1) == null)
            {
                rightHandButton.gameObject.SetActive(false);
            }
        }
        else
        {
            leftHandButton.gameObject.SetActive(true);
            rightHandButton.gameObject.SetActive(true);
        }
    }

    private void InteractButton(int hand)
    {

        if(item != null)
        {
            item.SetOutlineBool(false);
            item.SetForceOutline(false);
            gameObject.SetActive(false);
        }



        Food food = item.GetComponent<Food>();
        if(food != null)
        {
            GameObject prefab = food.GetKitchenElement().GetPrefab();
            PlayerSingleton.instance.GetComponent<Inventory>().SetHand(hand, food);
            return;
        }

        Container container = item.GetComponent<Container>();
        if (container != null)
        {
            Inventory inv = PlayerSingleton.instance.GetComponent<Inventory>();
            Food handItem = inv.GetHand(hand);

            if (handItem.CanBeCooked() && container.CanAddItem())
            {
                container.AddItem(handItem);

                handItem.SetPosItemDrop(item.transform.position);
                inv.SetHand(hand, null);
            }
            return;
        }

        Thrash thrash = item.GetComponent<Thrash>();
        if (thrash != null)
        {
            Inventory inv = PlayerSingleton.instance.GetComponent<Inventory>();
            Food handItem = inv.GetHand(hand);


            inv.SetHand(hand, null);
            Destroy(handItem.gameObject);
        }


        else
        {
            Inventory inv = PlayerSingleton.instance.GetComponent<Inventory>();
            Food handItem = inv.GetHand(hand);


            handItem.SetPosItemDrop(item.transform.position);
            inv.SetHand(hand, null);
        }

    }


    private void SetPosition()
    {
        Vector3 offset = new Vector3(1.5f, 1f, 0); 
        transform.position = item.transform.position + offset;

        transform.LookAt(Camera.main.transform);
    }


    public ItemShowingInvMenuInWorld GetItem()
    {
        return item;
    }
}
