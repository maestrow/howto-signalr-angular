using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HangfireSignalR.Models
{
    public enum TaskState
    {
        Completed = 0,
        Running = 1,
        Canceled = 2,
        Failed = 3
    }
}