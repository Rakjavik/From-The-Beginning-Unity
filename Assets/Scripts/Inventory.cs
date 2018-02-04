

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Inventory
{
    private GameObject owner;
    private List<GameObject> items;
    private int maxSize;
    


   public Inventory(int maxSize,GameObject owner)
    {
        items = new List<GameObject>();
        this.maxSize = maxSize;
        this.owner = owner;
    }

    public bool addItem(GameObject item)
    {
        if (hasEmptySpace())
        {
            items.Add(item);
            return true;
        } else
        {
            Debug.Log(owner.name + " tries to pick up " + item.name + ", but is full!");
            return false;
        }
    }
    public void removeItem(GameObject item)
    {
        items.Remove(item);
    }

    public List<GameObject> getItems()
    {
        return items;
    }

    public int listItems(bool toConsole)
    {
        if (toConsole)
        {
            Debug.Log("INVENTORY:");
            foreach (GameObject item in items)
            {
                Debug.Log(item.name);
            }
            Debug.Log("THAT'S IT");
        }
        return items.Count;
    }

    public bool hasEmptySpace()
    {
        if(items.Count < maxSize)
        {
            return true;
        }
        return false;
    }

    public GameObject get(int index)
    {
       return items[index];
    }

    public int getMaxInventorySize()
    {
        return maxSize;
    }
}