namespace rak.equipment
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public enum ItemType { RESOURCE, EQUIPMENT }
    public enum ResourceType { FOOD }

    public class Item
    {
        private ItemType itemType;
        private string name;
        private GameObject gameObject;

        protected float weight;

        public Item(string name, GameObject gameObject)
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
        public ItemType getItemType()
        {
            return itemType;
        }
        public float getWeight()
        {
            return weight;
        }
    }

    public class Inventory
    {
        private GameObject owner;
        private List<Item> items;
        private int maxSize;



        public Inventory(int maxSize, GameObject owner)
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
            }
            else
            {
                Debug.Log(owner.name + " tries to pick up " + item.getName() + ", but is full!");
                return false;
            }
        }
        public bool transferItem(Item item,Inventory targetInventory)
        {
            items.Remove(item);
            return targetInventory.addItem(item);
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
            if (items.Count < maxSize)
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

    public class Resource : Item
    {
        private ResourceType resourceType;

        public Resource(string name, GameObject gameObject, ResourceType resourceType) : base(name, gameObject)
        {
            this.resourceType = resourceType;
            if(resourceType == ResourceType.FOOD)
            {
                weight = .1f;
            }
        }

        public ResourceType getResourceType()
        {
            return resourceType;
        }

        public static Resource getNewInstance(string name, GameObject gameObject, ResourceType resourceType)
        {
            return new Resource(name, gameObject, resourceType);
        }
    }
}