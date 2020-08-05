using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    GameObject item;

    private void Start() {
        icon=GetComponent<Image>();
        text=gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void AddItem(GameObject newItem,Image image){
        item=newItem;
        icon=image;
    }
    public void ShowItem(){

    }
}
