using rak.work.job;

namespace rak.work.tasks
{
    public abstract class Tasks
    {

        public enum TaskType { IDLE,RESOURCE_GATHERING,NSFW};

        public static Task getNewTask(TaskType type,JobQueue jobQueue)
        {
            Task newTask = null;
            if(type == TaskType.IDLE)
            {
                newTask = new Task("Idle", "IDLE", false,TaskType.IDLE);
            }
            else if (type == TaskType.RESOURCE_GATHERING)
            {
                newTask = new Task("Gather Resources", "GATHER", false,TaskType.RESOURCE_GATHERING);
            }
            else if (type == TaskType.NSFW)
            {
                newTask = new Task("Boom Chicka Wow Wow", "NSFW", false,TaskType.NSFW);
            }
            jobQueue.addJobsToBottom(newTask.getJobList());
            return newTask;
        }
    }
}