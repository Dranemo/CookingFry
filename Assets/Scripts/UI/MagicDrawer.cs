using System.Collections.Generic;
using UnityEngine;

public class MagicDrawer : DrawerFloor
{
    [SerializeField] List<KitchenElement> elements;

    private void Awake()
    {
        int cellCount = Random.Range(0, 10);

        for(int i = 0; i < cellCount; i++)
        {
            int elementIndex = Random.Range(0, elements.Count);
            KitchenElement element = elements[elementIndex];
            AddKitchenElement(element);
        }
    }
}
