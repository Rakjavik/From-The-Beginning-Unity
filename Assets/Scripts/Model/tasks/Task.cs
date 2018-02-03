using rak.work.job;

namespace rak.work.tasks
{
    public class Task
    {
        private string taskName;
        private string shortName;
        private bool requirements;
        private Tasks.TaskType taskType;

        private Job[] jobList;

        public Task() { }
        public Task(string taskName,string shortName,bool requirements,Tasks.TaskType taskType)
        {
            this.taskName = taskName;
            this.shortName = shortName;
            this.requirements = requirements;
            this.taskType = taskType;
            jobList = generateJobList();
        }
        
        public string getTaskName()
        {
            return taskName;
        }
        public string getShortName()
        {
            return shortName;
        }
        public bool hasRequirements()
        {
            return requirements;
        }
        public bool isThisTask(Task taskName)
        {
            return taskName.Equals(this.taskName);
        }
        public Tasks.TaskType getTaskType()
        {
            return taskType;
        }
        public bool isThisTask(Tasks.TaskType taskType)
        {
            return this.taskType == taskType;
        }
        public Job[] getJobList()
        {
            return jobList;
        }

        private Job[] generateJobList()
        {
            Job[] jobs = null;
            if (taskType == Tasks.TaskType.RESOURCE_GATHERING) {
                jobs = new Job[2];
                Job job = new Job(this, Job.JobType.PickUp, Job.TargetType.Resource);
                jobs[0] = job;
                job = new Job(this, Job.JobType.DropOff,Job.TargetType.Base);
                jobs[1] = job;
            }
            else if (taskType == Tasks.TaskType.IDLE)
            {
                jobs = new Job[1];
                Job job = new Job(this, Job.JobType.IDLE, Job.TargetType.None);
                jobs[0] = job;
            }
            return jobs;
        }
    }
}