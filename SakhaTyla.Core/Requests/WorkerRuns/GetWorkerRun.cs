using System;
using MediatR;
using SakhaTyla.Core.Requests.WorkerRuns.Models;

namespace SakhaTyla.Core.Requests.WorkerRuns
{
    public class GetWorkerRun : IRequest<WorkerRunModel?>
    {
        public int Id { get; set; }
    }
}
