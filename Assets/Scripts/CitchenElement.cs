using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newElement", menuName = "KitchenElement/Element", order = 1)]
public class KitchenElement : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] GameObject prefab;
    [SerializeField] Sprite sprite;


    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public GameObject GetPrefab() {
        return prefab;
    }
}
