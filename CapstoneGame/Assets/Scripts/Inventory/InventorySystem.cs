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
    public Transform Player;
    public Dictionary<int, GameObject> ObjectPool = new Dictionary<int, GameObject>();
    public GameObject[] Slot;
    public GameObject slotHolder;
    public Camera FirstPersonCamera;
    public Camera ThirdPersonCamera;
    public bool running = true;
    //Item functions on pick up

    private void Start()
    {
        running = true;
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

        if (inventoryEnabled == true)
        {
            InvGraph.SetActive(true);
            if (FirstPersonCamera.isActiveAndEnabled)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                FirstPersonCamera cam = GameObject.FindGameObjectWithTag("FirstPersonCamera").GetComponent<FirstPersonCamera>();
                cam.cam = false;
            }
            if (ThirdPersonCamera.isActiveAndEnabled)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                CameraController cam = GameObject.FindGameObjectWithTag("ThirdPersonCamera").GetComponent<CameraController>();
                cam.cam = false;
            }

        }
        else
        {
            if (running) {  
                if (FirstPersonCamera.isActiveAndEnabled)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    FirstPersonCamera cam = GameObject.FindGameObjectWithTag("FirstPersonCamera").GetComponent<FirstPersonCamera>();
                    cam.cam = true;
                }
                if (ThirdPersonCamera.isActiveAndEnabled)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    CameraController cam = GameObject.FindGameObjectWithTag("ThirdPersonCamera").GetComponent<CameraController>();
                    cam.cam = true;
                }
            }
            InvGraph.SetActive(false);
        }
    }
}
