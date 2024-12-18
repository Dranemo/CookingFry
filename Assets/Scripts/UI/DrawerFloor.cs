using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerFloor : MonoBehaviour
{
    [SerializeField] List<KitchenElement> kitchenElements;
    [SerializeField] string name;

    public List<KitchenElement> KitchenElements => kitchenElements;
    public string GetName() => name;
}
