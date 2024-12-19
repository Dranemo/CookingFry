using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerSeeTable : MonoBehaviour
{
    [SerializeField] float rayLengthTable = 3f; // Longueur du rayon
    [SerializeField] float rayLengthFood = 3f; // Longueur du rayon



    [SerializeField] LayerMask layerMaskTable; // Masque de collision
    [SerializeField] LayerMask layerMaskFood; // Masque de collision


    [SerializeField] bool canDropItems = false;




    private Outlining lastOutliningItem;

    void FixedUpdate()
    {

        RaycastHit hit = new();
        RaycastHit hitDrop = new(); // Drop


        bool raycast = false;
        bool raycastDrop = false; // Drop


        if (PlayerSingleton.playerStats.WhatWatchingTable() != null)
        {
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            raycast = Physics.Raycast(ray, out hit, rayLengthFood, layerMaskFood);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * rayLengthFood, Color.red);
        }
        else
        { 
            Vector3 rayDirection = transform.forward;
            raycast = Physics.Raycast(transform.position, rayDirection, out hit, rayLengthTable, layerMaskTable);
            Debug.DrawRay(Camera.main.transform.position, rayDirection * rayLengthFood, Color.red);
        }

        if (canDropItems)                                                                                                                       // Drop
        {

            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            raycastDrop = Physics.Raycast(ray, out hitDrop, rayLengthFood, layerMaskTable);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * rayLengthFood, Color.red);                                         // Drop
        }


        if (raycast)
        {
            DropItem.instance.SetHit(raycastDrop, Vector3.zero);
            Outlining outliningItem = hit.collider.GetComponent<Outlining>();


            if (outliningItem != null)
            {
                if (lastOutliningItem != null && lastOutliningItem != outliningItem)
                {
                    lastOutliningItem.SetOutlineBool(false);
                }

                outliningItem.SetOutlineBool(true);
                lastOutliningItem = outliningItem;
            }
            else
            {
                if (lastOutliningItem != null)
                {
                    lastOutliningItem.SetOutlineBool(false);
                    lastOutliningItem = null;
                }
            }
        }
        else
        {
            Debug.Log("Le raycast n'a touché aucun objet.");


            if (lastOutliningItem != null)
            {
                lastOutliningItem.SetOutlineBool(false);
                lastOutliningItem = null;
            }

            if (PlayerSingleton.playerStats.WhatWatchingTable() != null && canDropItems)
            {
                if (raycastDrop) // Vérification si le raycast de drop a touché quelque chose
                {
                    Vector3 hitPos = hitDrop.point;
                    //Debug.Log("Position d'impact du drop: " + hitPos);

                    DropItem.instance.SetHit(raycastDrop, hitPos);
                }
                else
                {
                    DropItem.instance.SetHit(raycastDrop, Vector3.zero);
                    Debug.Log("Le raycast de drop n'a touché aucun objet.");
                }
            }
        }
    }










}
