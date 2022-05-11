using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Messaging.WorkerInfos
{
    public class ScheduleWorkerInfo
    {
        public static string QueueName => nameof(ScheduleWorkerInfo);

        public int Id { get; set; }
    }
}
