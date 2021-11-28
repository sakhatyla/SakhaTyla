using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.WorkerInfos
{
    public class CreateWorkerInfo : IRequest<CreatedEntity<int>>
    {
        public string Name { get; set; }

        public string ClassName { get; set; }
    }
}
