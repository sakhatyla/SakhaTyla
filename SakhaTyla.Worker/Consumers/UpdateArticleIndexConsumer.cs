using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using SakhaTyla.Core.Messaging.Articles;

namespace SakhaTyla.Worker.Consumers
{
    public class UpdateArticleIndexConsumer : IConsumer<UpdateArticleIndex>
    {
        private readonly IMediator _mediator;

        public UpdateArticleIndexConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<UpdateArticleIndex> context)
        {
            await _mediator.Send(context.Message);
        }
    }
}
