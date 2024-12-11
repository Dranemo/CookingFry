using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public static PlayerSingleton instance;
    public static PlayerStats playerStats;

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
}
