using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.WorkerInfos
{
    public class UpdateWorkerInfo : IRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? ClassName { get; set; }
    }
}
