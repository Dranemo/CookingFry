using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Food[] hands = new Food[2];

    public void SetHand(int hand, Food food)
    {
        hands[hand] = food;

        Debug.Log("SetHand: " + hand + " " + food);
    }
}
