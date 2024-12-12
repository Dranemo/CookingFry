using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemPooling : MonoBehaviour
{
    public static ItemPooling instance;
    private void Awake()
    {
        instance = this;
    }

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    public void CreatePoolItem(string tag, int amount)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            GameObject folder = new GameObject(tag + "Folder");


            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < amount; i++)
            {
                GameObject obj = new GameObject(tag);
                obj.SetActive(false);
                obj.transform.SetParent(folder.transform);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(tag, objectPool);
        }
    }



    public GameObject SpawnFromPool(string tag, Transform transformDestination, GameObject source, GameObject parent = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.transform.SetParent(parent.transform);
        objectToSpawn.transform.position = transformDestination.position;
        objectToSpawn.transform.rotation = transformDestination.rotation;
        objectToSpawn.transform.localScale = transformDestination.localScale;
        objectToSpawn.SetActive(true);

        // Ajouter dynamiquement les composants de l'objet prefab
        CopyComponents(tag, source, objectToSpawn);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot, Vector3 scale, GameObject source, GameObject parent = null)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();


        if(parent != null)
            objectToSpawn.transform.SetParent(parent.transform);


        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.rotation = rot;
        objectToSpawn.transform.localScale = scale;
        objectToSpawn.SetActive(true);

        // Ajouter dynamiquement les composants de l'objet prefab
        CopyComponents(tag, source, objectToSpawn);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }



    private void CopyComponents(string tag, GameObject source, GameObject destination)
    {
        destination.tag = source.tag;
        destination.layer = source.layer;

        Dictionary<Component, System.Type> tempDicToCopy = new Dictionary<Component, System.Type>();

        foreach (Component component in source.GetComponents<Component>())
        {
            if (component.GetType() == typeof(Transform))
            {
                continue;
            }

            System.Type type = component.GetType();
            Component copy = destination.AddComponent(type);

            tempDicToCopy.Add(copy, type);
        }

        foreach (Component component in source.GetComponents<Component>())
        {
            if(component.GetType() == typeof(Transform))
            {
                continue;
            }


            System.Type type = tempDicToCopy[component];
            foreach (var field in type.GetFields())
            {
                field.SetValue(tempDicToCopy[component], field.GetValue(component));
            }
        }


        foreach (Transform child in source.transform)
        {
            GameObject childObject = SpawnFromPool(tag, child, child.gameObject, source);
        }
    }

    public void ResetPooledItem(GameObject item)
    {
        item.SetActive(false);

        foreach (Transform child in item.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Component component in item.GetComponents<Component>())
        {
            if (component.GetType() != typeof(Transform))
            {
                Destroy(component);
            }
        }
    }
}
