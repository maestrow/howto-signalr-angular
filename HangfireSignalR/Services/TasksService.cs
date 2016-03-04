using System;
using System.Collections;
using System.Collections.Generic;
using HangfireSignalR.Models;

namespace HangfireSignalR.Services
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