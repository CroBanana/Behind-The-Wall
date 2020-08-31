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
    public GameObject slotItemShow;

    private void Awake(){
        icon=GetComponent<Image>();
        text=gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void AddItem(GameObject newItem,Sprite image,GameObject showItem){
        item=newItem;
        Debug.Log(image.name);
        icon.sprite=image;
        slotItemShow=showItem;

    }
    public void ShowItem(){
        slotItemShow.GetComponent<Image>().sprite=icon.sprite;
        slotItemShow.SetActive(true);
    }
}
