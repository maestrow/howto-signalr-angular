using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalRAngular.Models;
using SignalRAngular.Tasks;

namespace SignalRAngular.SignalRHubs
{
    public class TasksHub : Hub
    {
        private static Dictionary<TaskStatus, TaskState> statusMapping = new Dictionary<TaskStatus, TaskState>()
        {
            {TaskStatus.RanToCompletion, TaskState.Completed},
            {TaskStatus.Canceled, TaskState.Canceled},
            {TaskStatus.Faulted, TaskState.Failed}
        };

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

        public async Task<string> StartTask(string name, int delay, int failAfter)
        {
            var tokenSource = new CancellationTokenSource();

            string taskId = Guid.NewGuid().ToString();

            var item = new TaskProperties(taskId, name, tokenSource);
            item.State = TaskState.Running;
            CurrentTasks.TryAdd(taskId, item);

            Clients.All.taskStarted(item);

            var task = SampleAsyncTask.StartCalculation(delay, failAfter, tokenSource.Token, new Progress<int>(percent => onProgressChange(taskId, percent)));
            // ReSharper disable CSharpWarnings::CS4014
            task.ContinueWith(t => onTaskFinished(t, item), tokenSource.Token);
            task.ContinueWith(t => onTaskFinished(t, item), TaskContinuationOptions.OnlyOnCanceled);
            // ReSharper restore CSharpWarnings::CS4014
            await task;
            return taskId;
        }

        public void CancelTask(string taskId)
        {
            if (CurrentTasks.ContainsKey(taskId))
                CurrentTasks[taskId].CancelToken.Cancel();
        }

        private void onProgressChange(string taskId, int progress)
        {
            if (CurrentTasks.ContainsKey(taskId))
                CurrentTasks[taskId].Progress = progress;
            Clients.All.progressChanged(taskId, progress);
        }

        private void onTaskFinished(Task task, TaskProperties taskProps)
        {
            TaskState state = statusMapping[task.Status];
            taskProps.State = state;
            Clients.All.taskStateUpdated(taskProps.Id, state);
        }
    }
}