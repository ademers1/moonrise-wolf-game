using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject Items;
    public List<Item> Inventory = new List<Item>();
    public Item ItemScr;

    //Item functions on pick up
    
    private void OnCollisionEnter(Collision collision)
    {
        if(Items.tag == "Item")
        {
            pickUp(collision.gameObject);
        }
    }
    public void pickUp(GameObject I)
    {
        ItemScr = I.GetComponent<Item>();
        Inventory.Add(ItemScr);
        I.SetActive(false);
        Debug.Log("Item picked up");
    }
    public void drop(Item I)
    {
        Inventory.Remove(I);
    }

    private void Update()
    {
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
    }
}
