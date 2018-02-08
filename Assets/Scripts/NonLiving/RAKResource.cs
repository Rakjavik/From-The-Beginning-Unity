using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAKResource : MonoBehaviour,Item {

    public bool claimed = false;

    public Item getAsItem()
    {
        return (Item) this;
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
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
