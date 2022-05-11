using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks.Models
{
    public class WorkerScheduleTaskModel
    {
        public WorkerScheduleTaskModel(int workerInfoId)
        {
            WorkerInfoId = workerInfoId;
        }

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modification Date")]
        public DateTime ModificationDate { get; set; }

        [DisplayName("Creation User")]
        public int? CreationUserId { get; set; }
        public Users.Models.UserShortModel? CreationUser { get; set; }

        [DisplayName("Modification User")]
        public int? ModificationUserId { get; set; }
        public Users.Models.UserShortModel? ModificationUser { get; set; }

        [DisplayName("Worker")]
        public int WorkerInfoId { get; set; }
        public WorkerInfos.Models.WorkerInfoShortModel WorkerInfo { get; set; } = null!;

        [DisplayName("Seconds")]
        public string? Seconds { get; set; }

        [DisplayName("Minutes")]
        public string? Minutes { get; set; }

        [DisplayName("Hours")]
        public string? Hours { get; set; }

        [DisplayName("Day of Month")]
        public string? DayOfMonth { get; set; }

        [DisplayName("Month")]
        public string? Month { get; set; }

        [DisplayName("Day of Week")]
        public string? DayOfWeek { get; set; }

        [DisplayName("Year")]
        public string? Year { get; set; }
    }
}
