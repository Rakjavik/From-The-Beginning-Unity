using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDestination : MonoBehaviour {

    public float speed;

    public Vector3 destination;

	// Use this for initialization
	void Start () {
        Debug.Log(this.name + " initialized");
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(destination, transform.position);
        Debug.Log("Distance - " + distance);
        if (distance > 5.0f)
        {
            float amount = Time.deltaTime * speed;
            Vector3 nextLocation = Vector3.MoveTowards(transform.position, destination, amount);
            Vector3 newDirection = Vector3.RotateTowards(transform.position, destination, amount, 0.0F);
            Debug.DrawRay(transform.position, newDirection,Color.red);
            transform.rotation = Quaternion.LookRotation(newDirection);
            transform.position = nextLocation;
        } else
        {
            Debug.Log("Distance too short");
        }
	}

}
