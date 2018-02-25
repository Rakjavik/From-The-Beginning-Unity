using rak.unity.nonliving;
using rak.util;
using UnityEngine;

namespace rak.work.job
{
    public class Job
    {
        public enum JobType {PickUp,DropOff,IDLE,Consume,Locate};
        public enum TargetType {Base,Resource,None,Food};
        public enum AnimationType { Walk, Eat, Idle }

        private JobType jobType;
        private GameObject target;
        private TargetType targetType;
        private AnimationType animationType;
        private bool complete;

        public Job(JobType jobType,TargetType targetType)
        {
            initialize(jobType, targetType);
        }

        private void initialize(JobType jobType, TargetType targetType)
        {
            this.targetType = targetType;
            this.jobType = jobType;
            complete = false;
            if(jobType == JobType.IDLE)
            {
                animationType = AnimationType.Idle;
            }
            else if(jobType == JobType.PickUp || jobType == JobType.DropOff)
            {
                animationType = AnimationType.Walk;
            }
            if (isEmpty())
            {
                complete = true;
            }
        }

        public bool isEmpty()
        {
            if(jobType == JobType.IDLE && targetType == TargetType.None)
            {
                return true;
            }
            return false;
        }

        public void getNewTarget(Transform transform)
        {
            if(targetType == TargetType.Base)
            {
                target = Util.FindClosest(Tags.TAG_BASE, transform);
            }
            else if (targetType == TargetType.Resource)
            {
                target = Util.FindClosest(Tags.TAG_RESOURCE, transform);
                if(target != null)
                {
                    target.GetComponent<RAKItem>().setClaimed(true);
                }
                // No resources available in area //
                else
                {
                    target = null;
                }
            }
            else if (targetType == TargetType.Food)
            {
                target = Util.FindClosestResource( transform, equipment.ResourceType.FOOD);
            }
            else
            {
                target = null;
            }
        }
        public GameObject getTarget()
        {
            return target;
        }

        public JobType getJobType()
        {
            return jobType;
        }

        public void markComplete()
        {
            complete = true;
        }

        public bool isComplete()
        {
            return complete;
        }

        public void setTarget(GameObject target)
        {
            this.target = target;
        }
    }
}