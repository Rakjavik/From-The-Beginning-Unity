

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ItemType { RESOURCE, EQUIPMENT }

public class Item
{
    private ItemType itemType;
    private string name;
    private GameObject gameObject;

    public Item(string name,GameObject gameObject)
    {
        this.name = name;
        this.gameObject = gameObject;
    }

    public string getName()
    {
        return name;
    }
    public GameObject getGameObject()
    {
        return gameObject;
    }
}

public class Inventory
{
    private GameObject owner;
    private List<Item> items;
    private int maxSize;
    


   public Inventory(int maxSize,GameObject owner)
    {
        items = new List<Item>();
        this.maxSize = maxSize;
        this.owner = owner;
    }

    public bool addItem(Item item)
    {
        if (hasEmptySpace())
        {
            items.Add(item);
            return true;
        } else
        {
            Debug.Log(owner.name + " tries to pick up " + item.getName() + ", but is full!");
            return false;
        }
    }
    public void removeItem(Item item)
    {
        items.Remove(item);
    }

    public List<Item> getItems()
    {
        return items;
    }

    public int listItems(bool toConsole)
    {
        if (toConsole)
        {
            Debug.Log("INVENTORY:");
            foreach (Item item in items)
            {
                Debug.Log(item.getName());
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

    public Item get(int index)
    {
       return items[index];
    }

    public int getMaxInventorySize()
    {
        return maxSize;
    }
}