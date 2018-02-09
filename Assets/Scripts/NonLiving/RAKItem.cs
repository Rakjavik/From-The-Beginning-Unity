using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAKItem : MonoBehaviour {

    public bool claimed = false; // Item has been targeted in a job to be picked up

    private Item item;

    private void Start()
    {
        item = new Item("Resource", gameObject);
    }

    public Item getItem()
    {
        return item;
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public ItemType getItemType()
    {
        throw new System.NotImplementedException();
    }

    public string getName()
    {
        return "Resource";
    }

    public bool isClaimed()
    {
        return claimed;
    }

    public void setClaimed(bool claimed)
    {
        this.claimed = claimed;
    }
}
