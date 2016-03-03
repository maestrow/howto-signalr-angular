using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HangfireSignalR.Models;

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
            Debug.WriteLine(model.TaskName);
            return View("Index", model);
        }
    }
}