using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HangfireSignalR.SignalRHubs
{
    public class TasksHub : Hub
    {
        public void Hello(string name)
        {
            Clients.All.hello(name);
        }
    }
}