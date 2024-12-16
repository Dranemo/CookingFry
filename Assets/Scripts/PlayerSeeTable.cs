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




    private Outlining lastOutliningItem;

    void FixedUpdate()
    {

        RaycastHit hit = new();


        bool raycast = false;


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


        if (raycast)
        {
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
            if (lastOutliningItem != null)
            {
                lastOutliningItem.SetOutlineBool(false);
                lastOutliningItem = null;
            }
        }
    }










}
