namespace rak.unity
{
    using UnityEngine;


    public class RAKInteractable : MonoBehaviour
    {

        protected Rigidbody rigidBody;
        protected bool origKinematic;
        protected Transform originalParent;

        // Use this for initialization
        void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            origKinematic = rigidBody.isKinematic;
            originalParent = transform.parent;
        }

        public void Pickup(RAKVRInputController controller)
        {
            //Make object kinematic
            //(Not effected by physics, but still able to effect other objects with physics)
            rigidBody.isKinematic = true;

            //Parent object to hand
            transform.SetParent(controller.gameObject.transform);
        }

        public void Release(RAKVRInputController controller)
        {
            //Make sure the hand is still the parent. 
            //Could have been transferred to anothr hand.
            if (transform.parent == controller.gameObject.transform)
            {
                //Return previous kinematic state
                rigidBody.isKinematic = origKinematic;

                //Set object's parent to its original parent
                if (originalParent != controller.gameObject.transform)
                {
                    //Ensure original parent recorded wasn't somehow the controller (failsafe)
                    transform.SetParent(originalParent);
                }
                else
                {
                    transform.SetParent(null);
                }

                //Throw object
                //rigidBody.velocity = controller.device.velocity;
                //rigidBody.angularVelocity = controller.device.angularVelocity;
            }

        }
    }
}