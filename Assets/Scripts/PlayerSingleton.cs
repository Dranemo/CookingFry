using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton instance;
    public static PlayerStats playerStats;

    [SerializeField] GameObject canvasPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playerStats = new PlayerStats();
        }
        else
        {
            Destroy(this);
        }
    }





    void SetActiveCanvasPlayer(bool value)
    {
        canvasPlayer.SetActive(value);
    }
}
