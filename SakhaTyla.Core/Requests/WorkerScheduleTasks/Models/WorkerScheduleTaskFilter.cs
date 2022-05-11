using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks.Models
{
    public class WorkerScheduleTaskFilter : EntityFilter
    {
        public int? WorkerInfoId { get; set; }
        public string? Seconds { get; set; }
        public string? Minutes { get; set; }
        public string? Hours { get; set; }
        public string? DayOfMonth { get; set; }
        public string? Month { get; set; }
        public string? DayOfWeek { get; set; }
        public string? Year { get; set; }
    }
}
