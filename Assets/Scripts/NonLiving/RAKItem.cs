namespace rak.unity.nonliving
{
    using equipment;
    using UnityEngine;

    public class RAKItem : MonoBehaviour
    {
        public bool claimed = false; // Item has been targeted in a job to be picked up
        private Item item;
        private Vector3 displaySize;

        void Start()
        {
            item = Resource.getNewInstance("Resource", gameObject, ResourceType.FOOD);
            GetComponent<Rigidbody>().mass = item.getWeight();
        }

        private void OnDestroy()
        {
            Debug.Log(name + " Destroyed");
        }

        public Item getItem()
        {
            return item;
        }

        public bool isClaimed()
        {
            return claimed;
        }

        public void setClaimed(bool claimed)
        {
            this.claimed = claimed;
        }

        public void setItem(Item item)
        {
            this.item = item;
        }
    }
}