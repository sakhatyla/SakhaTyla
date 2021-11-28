using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.WorkerInfos.Models
{
    public class WorkerInfoFilter : EntityFilter
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
    }
}
