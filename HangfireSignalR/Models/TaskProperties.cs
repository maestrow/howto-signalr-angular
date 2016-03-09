using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Newtonsoft.Json;

namespace HangfireSignalR.Models
{
    public class TaskProperties
    {
        public TaskProperties(string id, string name, CancellationTokenSource cancelToken)
        {
            Id = id;
            Name = name;
            CancelToken = cancelToken;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Progress { get; set; }
        public TaskState State { get; set; }

        [JsonIgnore]
        public CancellationTokenSource CancelToken { get; set; }
    }
}