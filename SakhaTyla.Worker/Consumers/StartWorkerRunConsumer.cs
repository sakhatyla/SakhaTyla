using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using SakhaTyla.Core.Messaging.WorkerRuns;
using SakhaTyla.Worker.WorkerInfos;

namespace SakhaTyla.Worker.Consumers
{
    public class StartWorkerRunConsumer : IConsumer<StartWorkerRun>
    {
        private readonly WorkerInfoSheduler _workerInfoSheduler;

        public StartWorkerRunConsumer(WorkerInfoSheduler workerInfoSheduler)
        {
            _workerInfoSheduler = workerInfoSheduler;
        }

        public async Task Consume(ConsumeContext<StartWorkerRun> context)
        {
            await _workerInfoSheduler.RunAsync(context.Message);
        }
    }
}
