using rak.util;
using rak.work.tasks;
using UnityEngine;

namespace rak.work.job
{
    public class Job
    {
        public enum JobType { PickUp,DropOff,IDLE };
        public enum TargetType {Base,Resource,None};

        private JobType jobType;
        private Task associatedTask;
        private GameObject target;
        private TargetType targetType;
        private bool complete;

        public Job() {
            complete = false;
        }


        public Job(Task associatedTask,JobType jobType,TargetType targetType)
        {
            complete = false;
            initialize(associatedTask, jobType, targetType);
        }

        private void initialize(Task associatedTask, JobType jobType, TargetType targetType)
        {
            this.targetType = targetType;
            this.associatedTask = associatedTask;
            this.jobType = jobType;
        }

        public void setTarget(GameObject target)
        {
            this.target = target;
        }
        public GameObject getTarget(Transform transform)
        {
            if(target == null)
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
                        target.GetComponent<RAKResource>().setClaimed(true);
                    }
                }
            }
            return target;
        }
        public bool completed()
        {
            return complete;
        }
        public void setComplete()
        {
            this.complete = true;
        }
        public bool isThisType(JobType type)
        {
            return type == this.jobType;
        }
        public TargetType getTargetType()
        {
            return targetType;
        }
    }
}