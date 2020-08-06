using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private void Awake() {
        instance = this;
    }

    public List<GameObject> items;

    InventorySlot[] slots;

    private void Start() {
        slots= transform.GetComponentsInChildren<InventorySlot>();
    }
    public void AddItem(GameObject item){
        items.Add(item);
        slots[items.Count-1].AddItem(item,item.GetComponent<Item>().image);
    }

    // Update is called once per frame

}
