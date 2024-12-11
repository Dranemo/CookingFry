using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject rightHandCameraTexture;
    [SerializeField] GameObject LeftHandCameraTexture;


    Food[] hands = new Food[2]; // Left Right
    GameObject[] handTextures = new GameObject[2]; // Left Right





    private void Awake()
    {
        handTextures[0] = LeftHandCameraTexture;
        handTextures[1] = rightHandCameraTexture;
    }






/*    public void SetHand(int hand, Food food)
    {
        if(hands[hand] != null)
        {
            hands[hand].transform.position = food.transform.position;
            hands[hand].ResetRotation();
        }



        hands[hand] = food;
        if (food != null)
        {
            food.transform.position = handTextures[hand].transform.position;
            food.SetInHandOffset(handTextures[hand].transform.forward);

            food.transform.rotation = handTextures[hand].transform.rotation;
            food.transform.rotation *= Quaternion.Euler(-30, hand == 1 ? -20 : 20, -90);

            food.transform.localScale = new Vector3(1, hand == 1 ? -1 : 1, 1);
        }



















        Debug.Log("SetHand: " + hand + " " + food);
    }
*/



    public void SetHand(int hand, Food food)
    {
        if(hands[hand] != null)
        {
            Destroy(hands[hand].gameObject);
        }



        if(food != null)
        {
            food.GetComponent<Outline>().enabled = false;

            GameObject duplicateFood = Instantiate(food.gameObject);
            Food duplicateFoodFood = duplicateFood.GetComponent<Food>();
            hands[hand] = duplicateFoodFood;


            duplicateFoodFood.transform.position = handTextures[hand].transform.position;
            duplicateFoodFood.SetInHandOffset(handTextures[hand].transform.forward);

            duplicateFoodFood.transform.rotation = handTextures[hand].transform.rotation;
            duplicateFoodFood.transform.rotation *= Quaternion.Euler(-30, hand == 1 ? -20 : 20, -90);

            duplicateFoodFood.transform.localScale = new Vector3(1, hand == 1 ? -1 : 1, 1);
        }
        else
        { 
            hands[hand] = null;
        }
    }
}
