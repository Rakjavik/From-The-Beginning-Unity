using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAKResource : MonoBehaviour {

    private bool claimed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
