using UnityEngine;
using UnityEngine.InputSystem;

public class Food : ItemShowingInvMenuInWorld
{
    [SerializeField] float inHandOffset = 1.5f;
    [SerializeField] float upOffset = 0.5f;
    [SerializeField] KitchenElement kitchenElement;

    [Space(10)]
    [Header("Food properties")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    [Space(10)]
    [Tooltip("Can be cooked")]
    [SerializeField] bool canBeCooked = false;
    [SerializeField] bool isCooked = false;
    [SerializeField] int cookingTime = 100;
    [SerializeField] Mesh cookedMaterial;
    [SerializeField] KitchenElement cookedKitchenElement;

    [Space(10)]
    [Tooltip("Can be burned")]
    [SerializeField] bool canBeBurned = false;
    [SerializeField] bool isBurned = false;
    [SerializeField] int burningTime = 150;
    [SerializeField] Mesh burnedMaterial;
    [SerializeField] GameObject fireParticles;
    [SerializeField] KitchenElement burnedKitchenElement;

    [Space(10)]
    [Tooltip("Can be cutted")]
    [SerializeField] bool canBeCutted = false;
    [SerializeField] GameObject cuttedFood;



    [SerializeField] Quaternion defaultTransformRot;
    [SerializeField] Vector3 defaultTransformScale = Vector3.one;

    public bool alreadyCloned = false;
    public Container container = null;

    new private void Awake()
    {
        base.Awake();


        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Bounds bounds = renderer.bounds;
            Vector3 upOffset = new Vector3(0, bounds.extents.y, 0); // Ajouter un offset supplémentaire si nécessaire

            Debug.Log(upOffset);
        }
    }

    


    protected override void Interact(InputAction.CallbackContext context)
    {
        if(IsPointerOverUIElement())
        {
            return;
        }



        if (forceOutline && !isOutlined)
        {
            canvaInvItem.GetComponent<CanvaInvItemScript>().SetItem(null);
            canvaInvItem.SetActive(false);

            forceOutline = false;
            SetOutlineBool(false);
            return;
        }


        if (isOutlined)
        {

            forceOutline = true;
        }
    }







    
    public void ResetTransform()
    {
        //transform.position = defaultTransformPos;
        transform.rotation = defaultTransformRot;
        transform.localScale = defaultTransformScale;
    }
    public void SetInHandOffset(Vector3 forward)
    {
        transform.position += forward * inHandOffset;
    }
    public void SetPosItemDrop(Vector3 pos)
    {
        ResetTransform();
        transform.position = pos + Vector3.up * upOffset;
    }

    public KitchenElement GetKitchenElement()
    {
        return kitchenElement;
    }


    float cookedTime = 0;
    public void Cooking(float time)
    {
        Debug.Log("Cooking: " + cookedTime);

        if (canBeCooked && !isCooked)
        {
            cookedTime += time;
            if (cookedTime >= cookingTime)
            {
                SetCooked();
            }
        }
        else if(canBeBurned && !isBurned)
        {
            cookedTime += time;
            if (cookedTime >= burningTime)
            {
                SetBurned();
            }
        }
    }


    public bool CanBeCooked()
    {
        return canBeCooked;
    }   






    public void SetCooked()
    {
        isCooked = true;
        cookedTime = 0;
        meshFilter.mesh = cookedMaterial;
        meshCollider.sharedMesh = cookedMaterial;

        kitchenElement = cookedKitchenElement;
    }

    public void SetBurned()
    {
        isBurned = true;
        cookedTime = 0;
        meshFilter.mesh = burnedMaterial;
        meshCollider.sharedMesh = burnedMaterial;
        fireParticles.SetActive(true);

        kitchenElement = burnedKitchenElement;
    }
}
