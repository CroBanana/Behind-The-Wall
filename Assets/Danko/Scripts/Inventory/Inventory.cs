using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    static public List<GameObject> items;
    static public GameObject showItem;

    static InventorySlot[] slots;

    private void Start()
    {
        if (items == null)
        {
            items = new List<GameObject>();
        }
        if(slots==null){
            slots = transform.GetComponentsInChildren<InventorySlot>();
        }
        if(showItem==null){
            showItem=GameObject.Find("SlotItem");
            Debug.Log("SHOWITEM "+showItem.name);
            showItem.SetActive(false);
        }
    }
    public static void AddItem(GameObject item)
    {
        items.Add(item);
        slots[items.Count - 1].AddItem(item, item.GetComponent<Item>().image, showItem);
    }

    // Update is called once per frame

}
