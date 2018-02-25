using Boo.Lang;
using rak.unity.nonliving;
using UnityEngine;

namespace Model
{
    public class RAKRoom : MonoBehaviour
    {
        public int maxUnclaimedResources = 5;
        public Transform playerTransform;

        private float updateEvery = 5;
        private float lastUpdated;
        private bool maxUnclaimedReached;

        private void Start()
        {
            lastUpdated = 0;
        }

        void Update()
        {
            lastUpdated += Time.deltaTime;
            if (lastUpdated >= updateEvery)
            {
                lastUpdated = 0;
                List<GameObject> unclaimedResources = new List<GameObject>();
                for (int count = 0; count < transform.GetChildCount(); count++)
                {
                    if (transform.GetChild(count).tag == Tags.TAG_RESOURCE)
                    {
                        if (!transform.GetChild(count).GetComponent<RAKItem>().isClaimed())
                        {
                            unclaimedResources.Add(transform.GetChild(count).gameObject);
                        }
                    }                    
                }

                if (unclaimedResources.Count > maxUnclaimedResources)
                {
                    maxUnclaimedReached = true;
                }
                else
                {
                    maxUnclaimedReached = false;
                }
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) 
                                                                  || Input.GetKey(KeyCode.S))
            {
                Vector3 direction = Vector3.zero;
                bool valid = false;
                if (Input.GetKey(KeyCode.W))
                {
                    valid = true;
                    direction = -playerTransform.forward;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    valid = true;
                    direction = playerTransform.forward;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    valid = true;
                    direction = playerTransform.right;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    valid = true;
                    direction = -playerTransform.right;
                }
                if (valid)
                {
                    playerTransform.position += direction * Time.deltaTime * 15f;
                    
                }
                Debug.Log("New player transofrm - " + playerTransform.position);
            }
        }
        
        public bool MaxUnclaimedReached
        {
            get { return maxUnclaimedReached; }
        }
    }
}