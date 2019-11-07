using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Damage;
    public int ID;
    public string type;
    public string description;
    public Sprite icon;
    public bool pickedUp;

    InventorySystem Inventory;

    [HideInInspector]
    public bool equipped;
    [HideInInspector]
    public GameObject weapon;
    [HideInInspector]
    public GameObject weaponManager;

    public bool playersWeapon;

    private void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
        weaponManager = GameObject.FindWithTag("weaponManager");
        if(!playersWeapon)
        {
            int allWeapons = weaponManager.transform.childCount;
            for(int i=0;i<allWeapons;i++)
            {
                if (weaponManager.transform.GetChild(i).gameObject.GetComponent<Item>().ID == ID)
                {
                    weapon = weaponManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }
    public void Update()
    {
        if (equipped)
        {
            //perform weapon acts
            if(Input.GetKeyDown(KeyCode.G))
            {
                equipped = false;
            }
            if(equipped == false)
            {
                this.gameObject.SetActive(false);

            }

        }
    }

    public void DropItem(int id)
    {
        /*GameObject item;
        if (Inventory.ObjectPool.TryGetValue(id, out item))
        {

            item.transform.position = Inventory.Player.position + new Vector3(5, 0, 0);
            item.SetActive(true);
        }
        */
        int position = 0;
        for (int i = 0; i < Inventory.Slot.Length; i++) {
            if(Inventory.Slot[i].GetComponent<Slot>().ID == id)
            {
                position = i;
            }
        }
        Inventory.Slot[position].GetComponent<Slot>().empty = true;
        GameObject item = Inventory.Slot[position].GetComponent<Slot>().item;
        item.transform.position = Inventory.Player.position + new Vector3(5, 0, 0);
        item.GetComponent<Item>().pickedUp = false;
        item.SetActive(true);
        
        
    }
}
