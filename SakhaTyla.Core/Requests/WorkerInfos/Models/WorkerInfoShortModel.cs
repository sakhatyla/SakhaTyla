using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.WorkerInfos.Models
{
    public class WorkerInfoShortModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
