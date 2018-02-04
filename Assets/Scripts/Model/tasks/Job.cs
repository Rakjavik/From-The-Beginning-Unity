using rak.util;
using rak.work.tasks;
using UnityEngine;

namespace rak.work.job
{
    public class Job
    {
        public enum JobType {PickUp,DropOff,IDLE};
        public enum TargetType {Base,Resource,None};

        private JobType jobType;
        private Task associatedTask;
        private GameObject target;
        private TargetType targetType;
        private bool complete;

        public Job() {
            complete = false;
        }
        public Job(JobType jobType,TargetType targetType)
        {
            this.jobType = jobType;
            this.targetType = targetType;
        }

        public Job(Task associatedTask,JobType jobType,TargetType targetType)
        {
            initialize(associatedTask, jobType, targetType);
        }

        private void initialize(Task associatedTask, JobType jobType, TargetType targetType)
        {
            this.targetType = targetType;
            this.associatedTask = associatedTask;
            this.jobType = jobType;
            complete = false;
        }
        public static Job getEmpty()
        {
            Job job = new Job(JobType.IDLE,TargetType.None);
            return job;
        }
        public bool isEmpty()
        {
            if(jobType == JobType.IDLE && targetType == TargetType.None)
            {
                return true;
            }
            return false;
        }
        public void setTarget(GameObject target)
        {
            this.target = target;
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
                    target.GetComponent<RAKResource>().setClaimed(true);
                }
                // No resources available in area //
                else
                {
                    target = null;
                }
            }
            else if (targetType == TargetType.None)
            {
                target = null;
            }
        }
        public GameObject getTarget()
        {
            return target;
        }
        public bool hasTarget()
        {
            if(target != null)
            {
                return true;
            }
            return false;
        }
        public bool isThisType(JobType type)
        {
            return type == this.jobType;
        }
        public TargetType getTargetType()
        {
            return targetType;
        }
        public bool isCompleted()
        {
            return complete;
        }
        public Task getAssociateTask()
        {
            return associatedTask;
        }
    }
}