using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HangfireSignalR.Models;
using HangfireSignalR.SignalRHubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace HangfireSignalR.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(new TaskProperties());
        }

        public ActionResult StartTask(TaskProperties model)
        {
            IHubConnectionContext<dynamic> clients = GlobalHost.ConnectionManager.GetHubContext<TasksHub>().Clients;
            clients.All.hello(model.Name);
            return View("Index", model);
        }
    }
}