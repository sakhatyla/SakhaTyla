using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.WorkerRuns.Models;

namespace SakhaTyla.Core.Requests.WorkerRuns
{
    public class ExportWorkerRuns : IRequest<FileContentModel>
    {
        public WorkerRunFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
