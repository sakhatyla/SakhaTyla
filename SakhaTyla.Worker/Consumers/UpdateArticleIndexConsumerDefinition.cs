using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Definition;
using SakhaTyla.Core.Messaging.Articles;

namespace SakhaTyla.Worker.Consumers
{
    class UpdateArticleIndexConsumerDefinition : ConsumerDefinition<UpdateArticleIndexConsumer>
    {
        protected UpdateArticleIndexConsumerDefinition()
        {
            EndpointName = UpdateArticleIndex.QueueName;
        }
    }
}
