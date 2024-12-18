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

    public void SetItem(ItemShowingInvMenuInWorld _item, bool _showBothButton = true)
    {
        item = _item;

        if(item != null)
        {
            SetPosition();
        }



        
    }


    private void OnEnable()
    {
        if (!showBothButton)
        {
            Inventory inv = PlayerSingleton.instance.GetComponent<Inventory>();
            if (inv.GetHand(0) != null)
            {
                leftHandButton.gameObject.SetActive(false);
            }
            if (inv.GetHand(1) != null)
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
            PlayerSingleton.instance.GetComponent<Inventory>().SetHand(hand, food);
            return;
        }

        else
        {
            Inventory inv = PlayerSingleton.instance.GetComponent<Inventory>();
            Food handItem = inv.GetHand(hand);

            inv.SetHand(hand, null);

            handItem.SetPosItemDrop(item.transform.position);
        }

    }


    private void SetPosition()
    {
        Vector3 offset = new Vector3(1.5f, 0, 0); 
        transform.position = item.transform.position + offset;

        transform.LookAt(Camera.main.transform);
    }
}
