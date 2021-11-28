using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.WorkerRuns.Models
{
    public class WorkerRunShortModel
    {
        public int Id { get; set; }

        public string Data { get; set; }

        public override string ToString()
        {
            return $"{Data}";
        }
    }
}
