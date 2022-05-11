using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class WorkerScheduleTask : BaseEntity
    {
        public WorkerScheduleTask(int workerInfoId)
        {
            WorkerInfoId = workerInfoId;
        }

        [Required()]
        public int WorkerInfoId { get; set; }
        public WorkerInfo WorkerInfo { get; set; } = null!;
                
        [StringLength(50)]
        public string? Seconds { get; set; }
                
        [StringLength(50)]
        public string? Minutes { get; set; }
                
        [StringLength(50)]
        public string? Hours { get; set; }
                
        [StringLength(50)]
        public string? DayOfMonth { get; set; }
                
        [StringLength(50)]
        public string? Month { get; set; }
                
        [StringLength(50)]
        public string? DayOfWeek { get; set; }
                
        [StringLength(50)]
        public string? Year { get; set; }
                
    }
}
