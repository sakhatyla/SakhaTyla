using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.WorkerInfos.Models;

namespace SakhaTyla.Core.Requests.WorkerInfos
{
    public class ExportWorkerInfos : IRequest<FileContentModel>
    {
        public WorkerInfoFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
