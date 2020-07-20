using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public bool empty;
    public Sprite icon;
    public int ID;
    public string type;
    public string description;
    public GameObject item;
    public Transform slotIconGO;
    GameObject player;
    CharacterHealth charScr;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(slotIconGO.GetComponent<Image>().sprite != null)
        {
            UseItem();
        }
    }
    private void Start()
    {
        slotIconGO = transform.GetChild(0);
        player = GameObject.Find("Player");
        charScr = player.GetComponent<CharacterHealth>();
    }
    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
    }
    public void UseItem()
    {
        //heal player if HP is not full; right now item1 heals for 20 HP;
        if(item.GetComponent<Item>().type=="healing")
        {
            if(charScr.health != charScr.startHealth)
            {
                charScr.Heal(item.GetComponent<Item>().healAmount);
                item.GetComponent<Item>().destroyItem(ID);
                slotIconGO.GetComponent<Image>().sprite = null;
                Debug.Log("Current Health: " + charScr.health);
            }
        }
        //drop item if player HP is full
        else
        {
            item.GetComponent<Item>().DropItem(ID);
            slotIconGO.GetComponent<Image>().sprite = null;
            Debug.Log("Current Health: " + charScr.health);
        }
    }
}
