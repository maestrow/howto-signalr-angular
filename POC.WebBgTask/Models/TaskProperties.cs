using System.Threading;
using Newtonsoft.Json;

namespace POC.WebBgTask.Models
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