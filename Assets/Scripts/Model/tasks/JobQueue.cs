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
            currentJob = Job.getEmpty();
        }

        public Job getCurrentJob()
        {
            return currentJob;
        }
        public GameObject getCurrentJobTarget()
        {
            return currentJob.getTarget();
        }
        public int addJobsToBottom(Job[] jobs)
        {
            foreach(Job job in jobs)
            {
                jobList.Add(job);
            }
            if(currentJob.isEmpty())
            {
                currentJob = jobList[0];
                jobList.Remove(currentJob);
            }
            return jobList.Count;
        }

        // Completed current job, removes it from job queue and sets the next job, or completed the current task //
        public void completeCurrentJob(Task currentTask)
        {
            if (jobList.Count > 0)
            {
                currentJob = jobList[0];
                jobList.Remove(currentJob);
                currentJob.getNewTarget(agent.transform);
            }
            else
            {
                currentTask.markComplete();
                currentJob = Job.getEmpty();
            }
        }

        // Task was completed, Get a new task and set the current job, and job target //
        public Task getNextTask()
        {
            Task task;
            // Resources exist //
            if(Util.FindClosest(Tags.TAG_RESOURCE,agent.transform) != null)
            {
                task = Tasks.getNewTask(Tasks.TaskType.RESOURCE_GATHERING, this);
                
                // Starts with pickup, let's see if that's possible //
                if (!agent.getInventory().hasEmptySpace())
                {
                    // Skip to drop off //
                    currentJob = jobList[0];
                    jobList.RemoveAt(0);
                }
            }
            // No tasks, go Idle //
            else
            {
                task = Tasks.getNewTask(Tasks.TaskType.IDLE, this);
                
            }
            currentJob.getNewTarget(agent.transform);
            return task;
        }
        // Searches job list and removes Jobs generated from the task //
        private void removeTaskJobs(Task task)
        {
            for(int count = 0; count < jobList.Count; count++)
            {
                if(jobList[count].getAssociateTask() == task)
                {
                    jobList.Remove(jobList[count]);
                }
            }
        }
    }
}