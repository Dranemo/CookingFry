using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaInvItemScript : MonoBehaviour
{
    [SerializeField] Button rightHandButton;
    [SerializeField] Button leftHandButton;

    Food food = null;

    private void Awake()
    {
        rightHandButton.onClick.AddListener(() => SetHand(1, food));
        leftHandButton.onClick.AddListener(() => SetHand(0, food));
    }

    public void SetFood(Food _food)
    {
        food = _food;

        if(food != null)
        {
            SetPosition();
        }
    }

    private void SetHand(int hand, Food _food)
    {
        PlayerSingleton.instance.GetComponent<Inventory>().SetHand(hand, _food);
    }


    private void SetPosition()
    {
        Vector3 offset = new Vector3(1.5f, 0, 0); 
        transform.position = food.transform.position + offset;

        transform.LookAt(Camera.main.transform);
    }
}
