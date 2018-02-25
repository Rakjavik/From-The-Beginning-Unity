using rak.work.job;

namespace rak.work.tasks
{
    public abstract class Tasks
    {

        public enum TaskType { IDLE,RESOURCE_GATHERING,NSFW,EAT};

        public static Task getNewTask(TaskType type)
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
            else if (type == TaskType.EAT)
            {
                newTask = new Task("Eat", "EAT", false, TaskType.EAT);
            }
            return newTask;
        }
    }
}