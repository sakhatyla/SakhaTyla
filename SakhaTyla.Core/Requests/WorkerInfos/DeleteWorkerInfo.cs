using System;
using MediatR;

namespace SakhaTyla.Core.Requests.WorkerInfos
{
    public class DeleteWorkerInfo : IRequest
    {
        public int Id { get; set; }
    }
}
