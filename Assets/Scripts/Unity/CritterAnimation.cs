namespace rak.unity
{
    using rak.being.species.critter;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CritterAnimation : MonoBehaviour
    {
        private Animator animator;
        private BeingAgent agent;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            agent = GetComponentInParent<BeingAgent>();
            setGroundedAnimationVariable(true);
        }

        void Update()
        {
            setGroundedAnimationVariable(agent.isGrounded());
            var target = agent.getJobQueue().getCurrentJobTarget();
            if (isGrounded() && target != null)
            {
                
                Vector3 targetDir = target.transform.position - transform.parent.position;
                float step = Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.parent.forward, targetDir, step, 0.0F);
               
                transform.parent.rotation = Quaternion.LookRotation(newDir);
                foreach(RaycastHit hit in Physics.RaycastAll(transform.parent.position, transform.parent.forward))
                {
                    if (hit.collider.transform.Equals(target.transform))
                    {
                        setPointedAtTarget(true);
                        return;
                    }
                }

                setPointedAtTarget(false);
            }
            else if (!isGrounded())
            {
                setPointedAtTarget(false);
            }
        }

        private void setGroundedAnimationVariable(bool grounded)
        {
            animator.SetBool("Grounded",grounded);
        }

        private bool isGrounded()
        {
            return agent.isGrounded();
        }
        private void setPointedAtTarget(bool pointedAtTarget)
        {
            animator.SetBool("PointedAtTarget", pointedAtTarget);
        }

        private bool isPointedAtTarget()
        {
            return animator.GetBool("PointedAtTarget");
        }

        public void OnAnimatorMove() {
            if (animator)
            {
                transform.parent.position += animator.deltaPosition;
                //transform.parent.rotation = animator.rootRotation;
            }
        }
        
        
    }
}