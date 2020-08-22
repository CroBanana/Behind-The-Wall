﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    static public List<GameObject> items;
    public List<GameObject> test;
    public GameObject showItem;

    InventorySlot[] slots;

    private void Start()
    {
        test=items;
        if (items == null)
        {
            items = new List<GameObject>();
        }else if(items.Count>0){
            int f=0;
            foreach (var item in items)
            {
                Debug.Log("WOW");
                slots[f].AddItem(item,item.GetComponent<Item>().image,showItem);
                f++;
            }
        }
        slots = transform.GetComponentsInChildren<InventorySlot>();
    }
    public void AddItem(GameObject item)
    {
        items.Add(item);
        test=items;
        slots[items.Count - 1].AddItem(item, item.GetComponent<Item>().image, showItem);
    }

    // Update is called once per frame

}
