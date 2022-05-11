using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public class UpdateWorkerScheduleTask : IRequest
    {
        public int Id { get; set; }

        public string? Seconds { get; set; }

        public string? Minutes { get; set; }

        public string? Hours { get; set; }

        public string? DayOfMonth { get; set; }

        public string? Month { get; set; }

        public string? DayOfWeek { get; set; }

        public string? Year { get; set; }
    }
}
