using System;
using MediatR;

namespace SakhaTyla.Core.Requests.WorkerRuns
{
    public class DeleteWorkerRun : IRequest
    {
        public int Id { get; set; }
    }
}
