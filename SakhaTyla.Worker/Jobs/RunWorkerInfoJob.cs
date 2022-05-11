using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Quartz;
using SakhaTyla.Core.Messaging.WorkerInfos;
using SakhaTyla.Worker.Infrastructure;

namespace SakhaTyla.Worker.Jobs
{
    public class RunWorkerInfoJob : IJob
    {
        public static string JobKey => nameof(RunWorkerInfoJob);

        private readonly IMediator _mediator;

        public RunWorkerInfoJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var message = (RunWorkerInfo)context.Trigger.JobDataMap[QuartzData.Message];
            await _mediator.Send(message);
        }
    }
}
