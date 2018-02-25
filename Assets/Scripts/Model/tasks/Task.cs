using Boo.Lang;
using rak.equipment;
using rak.work.job;

namespace rak.work.tasks
{
    public class Task
    {
        private string taskName;
        private string shortName;
        private bool requirements;
        private readonly Tasks.TaskType taskType;
        private readonly bool complete;
        private float timeBeforeCancelled;
        private float timeSpent;
        private List<Item> jobItems;

        private readonly Job[] taskJobs;

        public Task(string taskName,string shortName,bool requirements,Tasks.TaskType taskType)
        {
            this.taskName = taskName;
            this.shortName = shortName;
            this.requirements = requirements;
            this.taskType = taskType;
            taskJobs = generateJobList();
            complete = false;
            timeBeforeCancelled = 0; // No time limit
            timeSpent = 0;
            jobItems = new List<Item>();
        }

        public Tasks.TaskType getTaskType()
        {
            return taskType;
        }

        public bool isCompleted()
        {
            return complete;
        }

        public void setTimeBeforeCancelled(float timeBeforeCancelled)
        {
            this.timeBeforeCancelled = timeBeforeCancelled;
        }

        private Job[] generateJobList()
        {
            Job[] jobs = null;
            if (taskType == Tasks.TaskType.RESOURCE_GATHERING) {
                jobs = new Job[4];
                Job job = new Job(Job.JobType.Locate, Job.TargetType.Resource);
                jobs[0] = job;
                job = new Job(Job.JobType.PickUp, Job.TargetType.Resource);
                jobs[1] = job;
                job = new Job(Job.JobType.Locate, Job.TargetType.Base);
                jobs[2] = job;
                job = new Job(Job.JobType.DropOff, Job.TargetType.Base);
                jobs[3] = job;
            }
            else if (taskType == Tasks.TaskType.IDLE)
            {
                jobs = new Job[1];
                Job job = new Job(Job.JobType.IDLE, Job.TargetType.None);
                jobs[0] = job;
            }
            else if (taskType == Tasks.TaskType.EAT)
            {
                jobs = new Job[3];
                Job job = new Job(Job.JobType.Locate,Job.TargetType.Food);
                jobs[0] = job;
                job = new Job(Job.JobType.PickUp, Job.TargetType.Food);
                jobs[1] = job;
                job = new Job(Job.JobType.Consume, Job.TargetType.Food);
                jobs[2] = job;
            }
            return jobs;
        }

        public Job[] getTaskJobs()
        {
            return taskJobs;
        }

        public void addJobItem(Item item)
        {
            jobItems.Add(item);
        }

        public void removeJobItem(Item item)
        {
            jobItems.Remove(item);
        }

        public List<Item> getJobItems()
        {
            return jobItems;
        }
    }
}