using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.WorkerRuns.Models
{
    public class WorkerRunModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modification Date")]
        public DateTime ModificationDate { get; set; }

        [DisplayName("Creation User")]
        public int? CreationUserId { get; set; }
        public Users.Models.UserShortModel CreationUser { get; set; }

        [DisplayName("Modification User")]
        public int? ModificationUserId { get; set; }
        public Users.Models.UserShortModel ModificationUser { get; set; }

        [DisplayName("Worker")]
        public int WorkerInfoId { get; set; }
        public WorkerInfos.Models.WorkerInfoShortModel WorkerInfo { get; set; }

        [DisplayName("Status")]
        public Enums.WorkerRunStatus Status { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDateTime { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDateTime { get; set; }

        [DisplayName("Data")]
        public string Data { get; set; }

        [DisplayName("Result")]
        public string Result { get; set; }

        [DisplayName("Result Data")]
        public string ResultData { get; set; }
    }
}
