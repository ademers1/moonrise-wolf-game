using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
    }
    private void Start()
    {
        slotIconGO = transform.GetChild(0);
    }
    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
    }
    public void UseItem()
    {
        item.GetComponent<Item>().ItemUsage();
        slotIconGO.GetComponent<Image>().sprite = null;
        slotIconGO.GetComponent<Image>().enabled = false;
    }
}
