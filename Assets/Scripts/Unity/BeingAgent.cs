using rak.being;
using rak.being.species.critter;
using rak.equipment;
using rak.work.job;
using UnityEngine;

namespace rak.unity
{
    public class BeingAgent : MonoBehaviour
    {
        private Being being;
        private JobQueue jobQueue;
        private bool grounded;
        private float distanceToTargetValid;

        public Inventory inventory;
        
        private void Start()
        {
            Being being = new Critter("Critter",'n',gameObject,null);
            this.being = being;
            inventory = new Inventory(5,gameObject);
            jobQueue = new JobQueue(this);
            jobQueue.getNextTask();
            distanceToTargetValid = 5;
        }

        private void Update()
        {
            jobQueue.Update(Time.deltaTime);
        }

        public bool addToInventory(Item item)
        {
            return inventory.addItem(item);
        }
        
        void OnCollisionEnter(Collision theCollision)
        {
            if (theCollision.transform.name.Equals("Floor"))
            {
                grounded = true;
            }
        }
        void OnCollisionExit(Collision theCollision)
        {
            if (theCollision.transform.name.Equals("Floor"))
            {
                grounded = false;
            }
        }

        public Being getBeing()
        {
            return being;
        }

        public bool isGrounded()
        {
            return grounded;
        }

        public JobQueue getJobQueue()
        {
            return jobQueue;
        }
    }
}