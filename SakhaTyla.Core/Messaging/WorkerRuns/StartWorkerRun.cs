using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SakhaTyla.Core.Messaging.WorkerRuns
{
    public class StartWorkerRun : IRequest
    {
        public static string QueueName => nameof(StartWorkerRun);

        public StartWorkerRun(int workerRunId)
        {
            WorkerRunId = workerRunId;
        }

        public int WorkerRunId { get; set; }
    }
}
