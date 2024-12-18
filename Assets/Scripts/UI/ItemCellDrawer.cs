using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellDrawer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionName;
    [SerializeField] TextMeshProUGUI descriptionDescription;
    [SerializeField] Image descImage;

    KitchenElement kitchenElement;

    public bool isOpened = false;


    public void SetKitchenElement(KitchenElement kitchenElement)
    {
        this.kitchenElement = kitchenElement;
        GetComponent<Image>().sprite = kitchenElement.GetSprite();
    }

    public KitchenElement GetKitchenElement()
    {
        return kitchenElement;
    }


    private void OnDisable()
    {
        kitchenElement = null;
        GetComponent<Image>().sprite = null;
    }


    public void OpenDescription()
    {
        descriptionName.text = kitchenElement.GetName();
        descriptionDescription.text = kitchenElement.GetDescription();
        descImage.sprite = kitchenElement.GetSprite();    
        
        isOpened = true;
    }

    public void Instanciation(TextMeshProUGUI name, TextMeshProUGUI desc, Image image)
    {
        descriptionName = name;
        descriptionDescription = desc;
        descImage = image;
    }
}