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
        public string Id { get; set; }
        public string Name { get; set; }
        public int Percent { get; set; }
        
        [JsonIgnore]
        public CancellationTokenSource CancelToken { get; set; }
    }
}