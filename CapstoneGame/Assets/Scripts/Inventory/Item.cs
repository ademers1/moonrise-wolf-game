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

    [HideInInspector]
    public bool equipped;
    [HideInInspector]
    public GameObject weapon;
    [HideInInspector]
    public GameObject weaponManager;

    public bool playersWeapon;

    private void Start()
    {
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
    public void ItemUsage()
    {
        //weapon
        if(type == "weapon")
        {
            weapon.SetActive(true);
            weapon.GetComponent<Item>().equipped = true;
        }
    }
}
