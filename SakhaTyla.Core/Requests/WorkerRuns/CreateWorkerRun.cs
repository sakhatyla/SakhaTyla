using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.WorkerRuns
{
    public class CreateWorkerRun : IRequest<CreatedEntity<int>>
    {
        public int? WorkerInfoId { get; set; }

        public string Data { get; set; }
    }
}
