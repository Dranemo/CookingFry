using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.XR;

public class Inventory : MonoBehaviour
{
    [SerializeField] RenderTexture rightHandCameraTexture;
    [SerializeField] RenderTexture LeftHandCameraTexture;

    [SerializeField] Vector3 camPos = new Vector3(0, -10, 0);
    [SerializeField] Quaternion rightCamRot = Quaternion.Euler(0, 0, 0);
    [SerializeField] Quaternion leftCamRot = Quaternion.Euler(0, 180, 0);

    [SerializeField] RenderTextureManager rtManager;
    [SerializeField] LayerMask layerMask;


    Food[] hands = new Food[2]; // Left Right

    RenderTexture[] handTextures = new RenderTexture[2]; // Left Right
    Quaternion[] rotationTexture = new Quaternion[2]; // Left Right





    private void Awake()
    {
        handTextures[0] = LeftHandCameraTexture;
        handTextures[1] = rightHandCameraTexture;

        rotationTexture[0] = leftCamRot;
        rotationTexture[1] = rightCamRot;

        rtManager.CaptureRenderTexture(handTextures[0], camPos, rightCamRot, layerMask);
        rtManager.CaptureRenderTexture(handTextures[1], camPos, leftCamRot, layerMask);
    }






/*    public void SetHand(int hand, Food food)
    {
        if(hands[hand] != null)
        {
            hands[hand].ResetTransform();
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

    public bool IsHandEmpty(int hand)
    {
        return hands[hand] == null;
    }

    public void SetHand(int hand, Food food)
    {
        /*if(hands[hand] != null && food != null)
        {
            Debug.Log("Bubup non");
            return;
        }*/

        if (food != null)
        {
            food.GetComponent<Outline>().enabled = false;

            GameObject duplicateFood = Instantiate(food.gameObject);
            Food duplicateFoodFood = duplicateFood.GetComponent<Food>();
            hands[hand] = duplicateFoodFood;


            duplicateFoodFood.transform.position = camPos;
            duplicateFoodFood.SetInHandOffset(rotationTexture[hand] * Vector3.forward);

            duplicateFoodFood.transform.rotation = rotationTexture[hand];
            duplicateFoodFood.transform.rotation *= Quaternion.Euler(-30, hand == 1 ? -20 : 20, -90);

            duplicateFoodFood.transform.localScale = new Vector3(1, hand == 1 ? -1 : 1, 1);


            rtManager.CaptureRenderTexture(handTextures[hand], camPos, rotationTexture[hand], layerMask);

        }
        else
        { 
            hands[hand] = null;
        }
    }
}
