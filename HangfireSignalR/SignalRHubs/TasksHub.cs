using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HangfireSignalR.Models;
using HangfireSignalR.Tasks;
using Microsoft.AspNet.SignalR;

namespace HangfireSignalR.SignalRHubs
{
    public class TasksHub : Hub
    {
        private static ConcurrentDictionary<string, TaskProperties> _CurrentTasks;
        private ConcurrentDictionary<string, TaskProperties> CurrentTasks
        {
            get
            {
                if (_CurrentTasks == null)
                    _CurrentTasks = new ConcurrentDictionary<string, TaskProperties>();

                return _CurrentTasks;
            }
        }

        public IEnumerable<TaskProperties> GetAllTasks()
        {
            return CurrentTasks.Values;
        }

        public async Task<string> StartTask(string name, int delay)
        {
            var tokenSource = new CancellationTokenSource();

            string taskId = Guid.NewGuid().ToString();

            CurrentTasks.TryAdd(taskId, new TaskProperties(taskId, name, tokenSource));

            Clients.All.taskStarted(taskId);

            await SampleAsyncTask.StartCalculation(delay, tokenSource.Token, new Progress<int>(percent => onProgressChange(taskId, percent)));
            
            onTaskComplete(taskId);
            return taskId;
        }

        public void CancelTask(string taskId)
        {
            if (CurrentTasks.ContainsKey(taskId))
                CurrentTasks[taskId].CancelToken.Cancel();
        }

        private void onTaskComplete(string taskId)
        {
            TaskProperties value;
            if (CurrentTasks.ContainsKey(taskId))
                CurrentTasks.TryRemove(taskId, out value);
            Clients.All.taskCompleted(taskId);
        }

        private void onProgressChange(string taskId, int progress)
        {
            if (CurrentTasks.ContainsKey(taskId))
                CurrentTasks[taskId].Percent = progress;
            Clients.All.progressChanged(taskId, progress);
        }

    }
}