using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject Items;
    public List<GameObject> Inventory = new List<GameObject>();

    //Item functions on pick up
    private void OnCollisionEnter(Collision collision)
    {
        Items = collision.gameObject;
        if(Items.tag == "item1")
        {
            Inventory.Insert(0,Items) ;
            Debug.Log("item1 picked up");
        }
        if (Items.tag == "item2")
        {
            Inventory.Insert(1,Items);
            Debug.Log("item2 picked up");
        }
        if (Items.tag == "item3")
        {
            Inventory.Insert(2,Items);
            Debug.Log("item3 picked up");
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {

        }
    }
}
