using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Definition;
using SakhaTyla.Core.Messaging.WorkerRuns;

namespace SakhaTyla.Worker.Consumers
{
    public class StartWorkerRunConsumerDefinition : ConsumerDefinition<StartWorkerRunConsumer>
    {
        public StartWorkerRunConsumerDefinition()
        {
            EndpointName = StartWorkerRun.QueueName;
        }
    }
}
