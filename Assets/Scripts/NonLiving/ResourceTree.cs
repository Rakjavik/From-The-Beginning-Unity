using Model;
using UnityEngine;
using rak.equipment;
using rak.unity.nonliving;

public class ResourceTree : MonoBehaviour
{
    private RAKRoom room;
    
    private float dropResourceEvery;
    private ResourceType resourceType;
    private float lastDropped;
    private int lastDropLocation;

    private static int numberOfResourceDrops = 5;
    private static Vector3 offset;

    // Use this for initialization
    void Start()
    {
        resourceType = ResourceType.FOOD;
        dropResourceEvery = 3.5f;
        lastDropLocation = 0;
        lastDropped = 0;
        offset = new Vector3(0f, 0, 0.0f);
        room = GetComponentInParent<RAKRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastDropped > dropResourceEvery && !room.MaxUnclaimedReached)
        {
            lastDropped = Time.time;
            int randomDrop = new System.Random().Next(0, numberOfResourceDrops);
            while (randomDrop == lastDropLocation)
            {
                randomDrop = new System.Random().Next(0, numberOfResourceDrops);
            }
            lastDropLocation = randomDrop;
            Transform drop = transform.Find("Drop" + randomDrop);
            GameObject resource;
            if (resourceType == ResourceType.FOOD) {
                resource = (GameObject)Instantiate(Resources.Load("prefabs/Resource"), drop);
                resource.GetComponent<RAKItem>().setItem(Resource.getNewInstance("Food", resource.gameObject, ResourceType.FOOD));
            }
            else
            {
                return;
            }
            resource.transform.SetParent(transform.parent);
            resource.transform.position = transform.position + offset;
        }
    }
}
