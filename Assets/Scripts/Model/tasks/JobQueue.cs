using rak.being.psyche;
using rak.unity;
using rak.util;
using rak.work.tasks;
using System.Collections.Generic;
using rak.equipment;
using rak.unity.baseobject;
using rak.unity.nonliving;
using UnityEngine;

namespace rak.work.job {

    public class JobQueue
    {
        private int currentJobNumber;
        private BeingAgent agent;
        private Task currentTask;
        private float timeSpentInCurrentJob;

        public JobQueue(BeingAgent agent)
        {
            this.agent = agent;
            currentJobNumber = 0;
            currentTask = Tasks.getNewTask(Tasks.TaskType.IDLE);
            timeSpentInCurrentJob = 0;
        }

        public GameObject getCurrentJobTarget()
        {
            return currentTask.getTaskJobs()[currentJobNumber].getTarget();
        }
        
        // Task was completed, Get a new task and set the current job, and job target //
        public Task getNextTask()
        {
            Task task;
            Psyche psyche = agent.getBeing().getPsyche();

            if(psyche.getCurrentValue(PsycheNeedType.HUNGER) < 0)
            {
                task = Tasks.getNewTask(Tasks.TaskType.EAT);
                Debug.Log("MUNCHIES!!!!");
            }
            // Resources exist //
            else if(Util.FindClosest(Tags.TAG_RESOURCE,agent.transform) != null)
            {
                task = Tasks.getNewTask(Tasks.TaskType.RESOURCE_GATHERING);
                task.setTimeBeforeCancelled(60);
            }
            // No tasks, go Idle //
            else
            {
                task = Tasks.getNewTask(Tasks.TaskType.IDLE);
                
            }
            return task;
        }

        public void Update(float deltaTime)
        {
            Job currentJob = currentTask.getTaskJobs()[currentJobNumber];
            timeSpentInCurrentJob += deltaTime;
            if (currentTask.getTaskType() == Tasks.TaskType.IDLE && timeSpentInCurrentJob > 1)
            {
                timeSpentInCurrentJob = 0;
                currentTask = getNextTask();
            }
            else if (currentJob.getJobType() == Job.JobType.Locate)
            {
                currentJob.getNewTarget(agent.transform);

                if (currentJob.getTarget() == null)
                {
                    //Debug.Log("Couldn't locate target for job " + currentJob.getJobType());
                }
                else
                {
                    // Set target of next job //
                    currentTask.getTaskJobs()[currentJobNumber + 1].setTarget(currentJob.getTarget());
                    currentJob.markComplete();
                }
            }
            else if (currentJob.getJobType() == Job.JobType.DropOff)
            {
                float dist = (agent.transform.position - currentJob.getTarget().transform.position).magnitude;
                if (dist < 3f)
                {
                    BaseScript targetBase = currentJob.getTarget().GetComponent<BaseScript>();
                    if (agent.inventory.transferItem(currentTask.getJobItems()[0], targetBase.Inventory))
                    {
                        currentJob.markComplete();
                        timeSpentInCurrentJob = 0;
                    }
                }
            }
            else if (currentJob.getJobType() == Job.JobType.PickUp)
            {
                float dist = (agent.transform.position - currentJob.getTarget().transform.position).magnitude;
                if (dist < 1)
                {
                    RAKItem item = currentJob.getTarget().GetComponent<RAKItem>();
                    if(agent.addToInventory(item.getItem()))
                    {
                        currentTask.addJobItem(item.getItem());
                        item.GetComponent<Renderer>().enabled = false;
                        currentJob.markComplete();
                    }
                }
            }

            if (currentJob.isComplete())
            {
                currentJobNumber++;
            }
            if (currentTask.isCompleted() || currentJobNumber > currentTask.getTaskJobs().Length - 1)
            {
                currentJobNumber = 0;
                currentTask = getNextTask();
            }
        }
    }
}