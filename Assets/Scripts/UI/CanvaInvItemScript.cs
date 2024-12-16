using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaInvItemScript : MonoBehaviour
{
    [SerializeField] Button rightHandButton;
    [SerializeField] Button leftHandButton;

    ItemShowingInvMenuInWorld item = null;

    private void Awake()
    {
        rightHandButton.onClick.AddListener(() => InteractButton(1));
        leftHandButton.onClick.AddListener(() => InteractButton(0));
    }

    public void SetItem(ItemShowingInvMenuInWorld _item)
    {
        item = _item;

        if(item != null)
        {
            SetPosition();
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

    }


    private void SetPosition()
    {
        Vector3 offset = new Vector3(1.5f, 0, 0); 
        transform.position = item.transform.position + offset;

        transform.LookAt(Camera.main.transform);
    }
}
