using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks.Models
{
    public class WorkerScheduleTaskShortModel
    {
        public WorkerScheduleTaskShortModel(string? seconds)
        {
            Seconds = seconds;
        }

        public int Id { get; set; }

        public string? Seconds { get; set; }

        public override string ToString()
        {
            return $"{Seconds}";
        }
    }
}
