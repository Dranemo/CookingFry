using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Table : Outlining
{

    [SerializeField] GameObject virtualCamera;
    [SerializeField] protected List<GameObject> itemToDisable = new List<GameObject>();

    Coroutine coroutineRunning = null;


    public override void SetOutlineBool(bool value)
    {
        if (PlayerSingleton.playerStats.WhatWatchingTable() != null)
            return;

        base.SetOutlineBool(value);
    }






    protected override void Interact(InputAction.CallbackContext context) 
    {
        if (coroutineRunning == null)
        {
            if (isOutlined && PlayerSingleton.playerStats.WhatWatchingTable() == null)
            {
                coroutineRunning = StartCoroutine(WatchingTable(true));
            }
            else if (PlayerSingleton.playerStats.WhatWatchingTable() == this)
            {
                coroutineRunning = StartCoroutine(WatchingTable(false));
            }
        }
    }

    IEnumerator WatchingTable(bool _bool)
    {


        foreach (GameObject item in itemToDisable)
            item.SetActive(_bool);


        virtualCamera.SetActive(_bool);
        if (!_bool)
        {
            yield return new WaitForSeconds(0.5f);
        }

        
        PlayerSingleton.playerStats.SetWatchingTable(_bool, this);

        PlayerSingleton.instance.GetComponent<PlayerMovements>().enabled = !_bool;
        PlayerSingleton.instance.GetComponent<CameraPlayer>().enabled = !_bool;

        if (_bool)
        {
            isOutlined = false;
            outline.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }

        CanvaInvItemScript.instance.gameObject.SetActive(false);
        coroutineRunning = null;
    }
}
