using UnityEngine;

public class PlayerStats
{
    Table tableWatching = null;

    public void SetWatchingTable(bool value, Table table)
    {
        Debug.Log("SetWatchingTable: " + value);

        if (!value)
        {
            Cursor.lockState = CursorLockMode.Locked;
            tableWatching = null;
        }
        else if (value)
        {
            Cursor.lockState = CursorLockMode.None;
            tableWatching = table;
        }



        Cursor.visible = value;

    }
    public Table WhatWatchingTable()
    {
        return tableWatching;
    }
}
