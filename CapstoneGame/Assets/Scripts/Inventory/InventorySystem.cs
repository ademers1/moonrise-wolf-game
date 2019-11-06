using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    //public GameObject Items;
    public List<Item> Inventory = new List<Item>();
    public Item ItemScr;
    bool inventoryEnabled;
    public GameObject InvGraph;
    int allSlots;
    int enabledSlots;
    bool itemAdded;
    GameObject[] Slot;
    public GameObject slotHolder;
    public CameraController camera;
    //Item functions on pick up

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        allSlots = 40;
        Slot = new GameObject[allSlots];
        for(int i=0;i<allSlots;i++)
        {
            Slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (Slot[i].GetComponent<Slot>().item == null)
                Slot[i].GetComponent<Slot>().empty = true;
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            pickUp(collision.gameObject);
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();

            addItem(itemPickedUp, item.ID, item.type, item.description, item.icon);
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
    /*
    public void pickUp(GameObject I)
    {
        ItemScr = I.GetComponent<Item>();
        Inventory.Add(ItemScr);
        I.SetActive(false);
        Debug.Log("Item picked up");
    }
    */
    void addItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        for (int i=0; i<allSlots; i++)
        {
            if (Slot[i].GetComponent<Slot>().empty)
            {
                //add item to slot
                if (!itemObject.GetComponent<Item>().pickedUp)
                {
                    itemObject.GetComponent<Item>().pickedUp = true;
                    Slot[i].GetComponent<Slot>().item = itemObject;
                    Slot[i].GetComponent<Slot>().icon = itemIcon;
                    Slot[i].GetComponent<Slot>().type = itemType;
                    Slot[i].GetComponent<Slot>().ID = itemID;
                    Slot[i].GetComponent<Slot>().description = itemDescription;

                    itemObject.transform.parent = Slot[i].transform;
                    itemObject.SetActive(false);

                    Slot[i].GetComponent<Slot>().UpdateSlot();
                    Slot[i].GetComponent<Slot>().empty = false;
                }
            }
        }
    }
    /*
    public void drop(Item I)
    {
        Inventory.Remove(I);
    }
    */
    private void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Inventory.RemoveAt(0);
            Debug.Log("item 1 used");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Inventory.RemoveAt(1);
            Debug.Log("item 2 used");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Inventory.RemoveAt(3);
            Debug.Log("item 3 used");
        }
        */
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if(inventoryEnabled == true)
        {
            InvGraph.SetActive(true);
            //camera.cam = false;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //camera.cam = false;
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;

           // camera.cam = true;
            InvGraph.SetActive(false);
        }
    }
}
