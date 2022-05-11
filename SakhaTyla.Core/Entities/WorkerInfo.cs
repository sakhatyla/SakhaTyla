using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class WorkerInfo : BaseEntity
    {
        public WorkerInfo(string name, string className)
        {
            Name = name;
            ClassName = className;
        }

        [Required()]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required()]
        [StringLength(200)]
        public string ClassName { get; set; }

        public IList<WorkerScheduleTask> ScheduleTasks { get; set; } = null!;

    }
}
