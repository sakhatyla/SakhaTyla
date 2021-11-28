using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class WorkerRun : BaseEntity
    {
        [Required()]
        public int WorkerInfoId { get; set; }
        public WorkerInfo WorkerInfo { get; set; }
        
        [Required()]
        public Enums.WorkerRunStatus Status { get; set; }
        

        public DateTime? StartDateTime { get; set; }
        

        public DateTime? EndDateTime { get; set; }
        

        public string Data { get; set; }
        

        public string Result { get; set; }
        

        public string ResultData { get; set; }
        
    }
}
