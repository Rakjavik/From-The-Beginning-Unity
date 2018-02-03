
using UnityEngine;
using System.Collections;

namespace rak.unity.things {

    public class ResourceTree : MonoBehaviour
    {

        private float dropResourceEvery;
        private float lastDropped;

        // Use this for initialization
        void Start()
        {
            dropResourceEvery = 20f;
        }

        // Update is called once per frame
        void Update()
        {
            if(lastDropped-Time.time > dropResourceEvery)
            {
                lastDropped = Time.time;
                
            }
        }
    }
}
