using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class CanvasDrawers : MonoBehaviour
{
    List<DrawerFloor> floors;
    string name;
    [SerializeField] bool canPutAwayItem = false;


    [Header("Pool")]
    [SerializeField] List<GameObject> tabs;
    [SerializeField] GameObject tabPrefab;
    [SerializeField] List<GameObject> itemCells;
    List<GameObject> itemCellsActivated = new List<GameObject>();
    [SerializeField] GameObject itemCellPrefab;


    
    [Header("UI")]
    [Tooltip("Description of the item")]
    [SerializeField] GameObject desc;
    [SerializeField] TextMeshProUGUI descName;
    [SerializeField] TextMeshProUGUI descDescription;
    [SerializeField] Image descImage;
    [Tooltip("Other")]
    [SerializeField] TextMeshProUGUI drawerName;
    [SerializeField] List<GameObject> hands;
    [SerializeField] Button quitButton;

    [SerializeField] Color selectedTabColor;
    [SerializeField] Color unselectedTabColor;




    int tabSelected = 0;


    private void OnEnable()
    {
        FullReset();

        PlayerSingleton.instance.GetComponent<PlayerMovements>().enabled = false;
        PlayerSingleton.instance.GetComponent<CameraPlayer>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        drawerName.text = name;

        for (int i = 0; i < floors.Count; i++)
        {
            if(i >= tabs.Count)
            {
                GameObject tab = Instantiate(tabPrefab, tabs[0].transform.parent);

                tabs.Add(tab);

            }

            tabs[i].SetActive(true);
            tabs[i].GetComponentInChildren<TextMeshProUGUI>().text = floors[i].GetName();
        }

        Food hand = PlayerSingleton.instance.GetComponent<Inventory>().GetHand(0);
        if(hand != null)
            hands[0].GetComponent<Image>().sprite = hand.GetKitchenElement().GetSprite();

        hand = PlayerSingleton.instance.GetComponent<Inventory>().GetHand(1);
        if (hand != null)
            hands[1].GetComponent<Image>().sprite = hand.GetKitchenElement().GetSprite();



        // boutons
        quitButton.onClick.AddListener(CloseDrawer);
        for (int i = 0; i < tabs.Count; i++)
        {
            int index = i;
            tabs[i].GetComponent<Button>().onClick.AddListener(() => SetFloor(index));
        }
        for (int i = 0; i < hands.Count; i++)
        {
            int index = i;
            hands[i].transform.parent.GetComponent<Button>().onClick.AddListener(() => SetInHand(index));
        }

        SetFloor(0);
    }

    private void OnDisable()
    {
        PlayerSingleton.instance.GetComponent<PlayerMovements>().enabled = true;
        PlayerSingleton.instance.GetComponent<CameraPlayer>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        quitButton.onClick.RemoveListener(CloseDrawer);
        FullReset();
    }


    public void SetDrawer(string _name, List<DrawerFloor> _drawerFloors)
    {
        name = _name;
        floors = _drawerFloors;
    }


    void SetFloor(int floorIndex)
    {
        Reset();

        tabSelected = floorIndex;

        tabs[tabSelected].GetComponent<Image>().color = selectedTabColor;

        for (int i = 0; i < floors[tabSelected].KitchenElements.Count; i++)
        {
            if(i >= itemCells.Count)
            {
                GameObject itemCellPre = Instantiate(itemCellPrefab, itemCells[0].transform.parent);
                itemCellPre.GetComponentInChildren<ItemCellDrawer>().Instanciation(descName, descDescription, descImage);

                itemCells.Add(itemCellPre);
            }

            itemCells[i].SetActive(true);
            itemCellsActivated.Add(itemCells[i]);

            KitchenElement kitchenElement = floors[tabSelected].KitchenElements[i];
            ItemCellDrawer itemCell = itemCells[i].GetComponentInChildren<ItemCellDrawer>();

            itemCell.SetKitchenElement(kitchenElement);
            itemCells[i].GetComponent<Button>().onClick.AddListener(() => OpenDescription(itemCell));
        }
    }


    void CloseDrawer()
    {
        gameObject.SetActive(false);
    }

    private void Reset()
    {
        isHandSelected = 2;
        desc.SetActive(false);
        tabs[tabSelected].GetComponent<Image>().color = unselectedTabColor;
        tabSelected = 0;

        foreach (var item in itemCells)
        {
            item.SetActive(false);
            item.GetComponentInChildren<ItemCellDrawer>().isOpened = false;
            item.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        itemCellsActivated.Clear();

    }

    void FullReset()
    {
        Reset();
        foreach (var item in tabs)
        {
            item.SetActive(false);
        }
        foreach (var item in hands)
        {
            item.GetComponent<Image>().sprite = null;
        }
    }



    ItemCellDrawer lastCell = null;
    void OpenDescription(ItemCellDrawer cell)
    {
        isHandSelected = 2;
        if (desc.activeSelf && cell.isOpened)
        {
            desc.SetActive(false);

            lastCell.isOpened = false;
            lastCell = null;
        }
        else
        {
            desc.SetActive(true);

            if (lastCell != null)
                lastCell.isOpened = false;
            lastCell = cell;
            cell.OpenDescription();
        }
    }



    int isHandSelected = 2;
    void SetInHand(int index)
    {
        if(lastCell == null)
        {
            if(isHandSelected == index)
            {
                if (canPutAwayItem)
                {
                    Inventory inventory = PlayerSingleton.instance.GetComponent<Inventory>();
                    Food hand = inventory.GetHand(index);

                    if (hand != null)
                    {
                        hands[index].GetComponent<Image>().sprite = null;

                        floors[tabSelected].AddKitchenElement(hand.GetKitchenElement());

                        inventory.SetHand(index, null);


                        if (itemCellsActivated.Count >= itemCells.Count)
                        {
                            GameObject itemCellPre = Instantiate(itemCellPrefab, itemCells[0].transform.parent);
                            itemCellPre.GetComponentInChildren<ItemCellDrawer>().Instanciation(descName, descDescription, descImage);

                            itemCells.Add(itemCellPre);
                        }

                        itemCells[itemCellsActivated.Count].SetActive(true);
                        itemCellsActivated.Add(itemCells[itemCellsActivated.Count]);

                        KitchenElement kitchenElementhand = hand.GetKitchenElement();
                        ItemCellDrawer itemCell = itemCells[itemCellsActivated.Count - 1].GetComponentInChildren<ItemCellDrawer>();

                        itemCell.SetKitchenElement(kitchenElementhand);
                        itemCells[itemCellsActivated.Count - 1].GetComponent<Button>().onClick.AddListener(() => OpenDescription(itemCell));

                        Destroy(hand.gameObject);
                    }
                }
            }
            else
                isHandSelected = index;

            return;
        }













        Inventory inv = PlayerSingleton.instance.GetComponent<Inventory>();

        if (inv.GetHand(index) != null)
            return;

        KitchenElement kitchenElement = lastCell.GetKitchenElement();

        inv.SetHandPrefab(index, kitchenElement);


        hands[index].GetComponent<Image>().sprite = inv.GetHand(index).GetKitchenElement().GetSprite();

        OpenDescription(lastCell);
    }
}
