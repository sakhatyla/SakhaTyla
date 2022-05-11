using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Definition;
using SakhaTyla.Core.Messaging.WorkerInfos;

namespace SakhaTyla.Worker.Consumers
{
    class ScheduleWorkerInfoConsumerDefinition : ConsumerDefinition<ScheduleWorkerInfoConsumer>
    {
        protected ScheduleWorkerInfoConsumerDefinition()
        {
            EndpointName = ScheduleWorkerInfo.QueueName;
        }
    }
}
