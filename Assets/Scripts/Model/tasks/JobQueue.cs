using rak.unity;
using rak.util;
using rak.work.tasks;
using System.Collections.Generic;
using UnityEngine;

namespace rak.work.job {

    public class JobQueue
    {
        private List<Job> jobList;
        private Job currentJob;
        private Agent agent;

        public JobQueue(Agent agent)
        {
            this.agent = agent;
            jobList = new List<Job>();
        }

        public void addNewJobToBottom(Job job)
        {
            jobList.Add(job);
        }
        public void addNewJobToTop(Job job)
        {
            jobList.Insert(0, job);
        }
        public void removeJob(Job job)
        {
            jobList.Remove(job);
        }
        public void cancelCurrentJob()
        {
            currentJob = null;
        }
        public Job getCurrentJob()
        {
            if (currentJob == null || currentJob.completed() || currentJob.isThisType(Job.JobType.IDLE))
            {
                if (jobList.Count > 0)
                {
                    currentJob = jobList[0];
                    jobList.RemoveAt(0);
                }
                else
                {
                    return null;
                }
            }
            return currentJob;
        }
        public GameObject getCurrentJobTarget(Transform transform)
        {
            return currentJob.getTarget(transform);
        }
        public int addJobsToBottom(Job[] jobs)
        {
            foreach(Job job in jobs)
            {
                jobList.Add(job);
            }
            return jobList.Count;
        }

        public Task getNextTask()
        {
            Task task;
            if(Util.FindClosest(Tags.TAG_RESOURCE,agent.transform) || agent.getInventory().getItems().Count > 0)
            {
                task = Tasks.getNewTask(Tasks.TaskType.RESOURCE_GATHERING, this);
            }
            else
            {
                task = Tasks.getNewTask(Tasks.TaskType.IDLE, this);
            }
            return task;
        }
    }
}