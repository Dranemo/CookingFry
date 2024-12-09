using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSeeTable : MonoBehaviour
{
    [SerializeField] float rayLength = 3f; // Longueur du rayon
    [SerializeField] LayerMask layerMask; // Masque de collision
    private Table lastHitTable;

    void FixedUpdate()
    {
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;


        // Debug -------------------
        Debug.DrawRay(transform.position, rayDirection * rayLength, Color.red);
        Debug.Log(lastHitTable == null ? "" : lastHitTable.name);





        if (Physics.Raycast(transform.position, rayDirection, out hit, rayLength, layerMask))
        {
            Table table = hit.collider.GetComponent<Table>();

            if (table != null)
            {
                if (lastHitTable != null && lastHitTable != table)
                {
                    lastHitTable.SetOutlineBool(false);
                }

                table.SetOutlineBool(true);
                lastHitTable = table;
            }
            else
            {
                if (lastHitTable != null)
                {
                    lastHitTable.SetOutlineBool(false);
                    lastHitTable = null;
                }
            }
        }
        else
        {
            if (lastHitTable != null)
            {
                lastHitTable.SetOutlineBool(false);
                lastHitTable = null;
            }
        }
    }
}
