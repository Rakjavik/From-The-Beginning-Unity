using rak.being.species.critter;
using rak.unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RAKVRroom : MonoBehaviour{

    public bool displayPersonalViewScreens; // Whether agents should all display their personal viewscreens
    public bool debugRoom;

    private Agent selection; // Object that is currently selected/focused
    private bool waitingOnObjectMovement; // Room is waiting for dropped agent to land on floor and reinitialize.

	// Use this for initialization
	void Start () {
        selection = null;
        waitingOnObjectMovement = false;
        debug(gameObject.name + " Initializing object - " + gameObject.GetInstanceID());
	}
	
    public void setSelection(Agent selection)
    {
        this.selection = selection;
        debug("Set selection called on Room, ready - " + !waitingOnObjectMovement);
        if (selection != null)
        {
            debug("Selection target - " + selection.name);
        }
    }
    public Agent getSelection()
    {
        return selection;
    }
    public bool isSomethingSelected()
    {
        if(selection == null)
        {
            return false;
        } else
        {
            return true;
        }
    }
    public void dropSelection()
    {
        if (selection != null)
        {
            debug(gameObject.name + "- Dropping - " + selection.name);
            selection = null;
            waitingOnObjectMovement = true;
        } else {
            debug("Drop selection called with no selection");
        }
    }
    public void toggleWaitingOnObjectMovement()
    {
        waitingOnObjectMovement = !waitingOnObjectMovement;
        if(waitingOnObjectMovement)
        {
            selection = null;
        }
    }
    public bool isWaitingOnObjectMovement()
    {
        debug("is waiting on object - " + waitingOnObjectMovement);
        return waitingOnObjectMovement;
    }

    public bool doDisplayPersonalViewScreens()
    {
        return displayPersonalViewScreens;
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void debug(string message)
    {
        if(debugRoom)
        {
            Debug.Log(message);
        }
    }
}
