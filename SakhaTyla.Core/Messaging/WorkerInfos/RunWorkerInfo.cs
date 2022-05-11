using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SakhaTyla.Core.Messaging.WorkerInfos
{
    public class RunWorkerInfo : IRequest
    {
        public int Id { get; set; }
        public object? Data { get; set; }
    }
}
