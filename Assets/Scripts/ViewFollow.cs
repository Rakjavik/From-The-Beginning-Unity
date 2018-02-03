
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFollow : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
        target = null;
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            Vector3 newRotation = Vector3.RotateTowards(transform.position, target.transform.position, 500, 500);
            transform.LookAt(target.transform);
        }
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }
}
