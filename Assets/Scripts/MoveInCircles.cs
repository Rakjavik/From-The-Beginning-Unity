using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircles : MonoBehaviour {

    public float speed;

    public int rotationAmount;

    public int timeInEachDirection;
        
    private int currentDirection = 0;

    private float timeMoved = 0.0f;

    public Rigidbody rb;

    public GameObject center;

    public float maxVelocity;

    private float x, z;

    // Use this for initialization
    void Start()
    {
        //rb.centerOfMass = center.transform.position;
        z = 0;
        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        timeMoved += Time.fixedDeltaTime;
        
        if (timeMoved >= timeInEachDirection)
        {
            transform.Rotate(new Vector3(0, -90, 0));
            timeMoved = 0;
            
            if (currentDirection == 0)
            {
                currentDirection++;
                x = -speed;
                z = 0;
            }
            else if (currentDirection == 1)
            {
                currentDirection++;
                z = -speed;
                x = 0;
            }
            else if (currentDirection == 2)
            {
                currentDirection++;
                x = speed;
                z = 0;
            }
            else
            {
                currentDirection = 0;
                z = speed;
                x = 0;
            }
        }

        Vector3 movement = new Vector3(x, 0.0f, z)*Time.fixedDeltaTime;
        Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude < maxVelocity)
        {
            rb.AddForce(movement);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -90, 0));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }

       
}
