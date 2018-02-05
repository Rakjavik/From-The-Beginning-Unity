using UnityEngine;
using rak.being;

public class RAKBodyPart : MonoBehaviour
{

    public string bodyPartName;
    private BodyPart bodyPart;
    private float timeDetached = 0;

    public string getBodyPartName()
    {
        return bodyPartName;
    }
    public void setBodyPart(BodyPart bodyPart)
    {
        this.bodyPart = bodyPart;
    }
    public void detach(bool freezeLimbs)
    {
        if (bodyPart.isAttached())
        {
            transform.SetParent(null);
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            gameObject.AddComponent<BoxCollider>();
            if(freezeLimbs)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.isKinematic = true;
                rb.position = rb.position + Vector3.right*.4f;
            }
        }
    }

    public void Update()
    {
        if(bodyPart != null && !bodyPart.isAttached())
        {
            timeDetached += Time.deltaTime;
            Debug.Log(timeDetached);
            if(timeDetached > 100)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
