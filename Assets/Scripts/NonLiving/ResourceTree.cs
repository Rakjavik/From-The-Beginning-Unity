using UnityEngine;
using System.Collections;
using UnityEditor;

public class ResourceTree : MonoBehaviour
{
    private float dropResourceEvery;
    private float lastDropped;
    private int lastDropLocation;

    private static int numberOfResourceDrops = 5;
    private static Vector3 offset;

    // Use this for initialization
    void Start()
    {
        dropResourceEvery = 5.0f;
        lastDropLocation = 0;
        lastDropped = 0;
        offset = new Vector3(-1.5f, 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastDropped > dropResourceEvery)
        {
            lastDropped = Time.time;
            int randomDrop = new System.Random().Next(0, numberOfResourceDrops);
            while (randomDrop == lastDropLocation)
            {
                randomDrop = new System.Random().Next(0, numberOfResourceDrops);
            }
            lastDropLocation = randomDrop;
            Transform drop = transform.Find("Drop" + randomDrop);
            GameObject resource = (GameObject)Instantiate(Resources.Load("prefabs/Resource"),drop);
            resource.transform.SetParent(this.transform.parent);
            resource.transform.position += offset;
        }
    }
}
