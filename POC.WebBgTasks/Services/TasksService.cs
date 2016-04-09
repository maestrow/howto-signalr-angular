using System;

namespace POC.WebBgTasks.Services
{
    public class TasksService
    {
        private static Lazy<TasksService> _instance = new Lazy<TasksService>(() => new TasksService());

        public TasksService Instance
        {
            get { return _instance.Value; }
        }

    }
}