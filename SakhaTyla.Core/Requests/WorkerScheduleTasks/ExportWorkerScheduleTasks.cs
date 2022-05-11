using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public class ExportWorkerScheduleTasks : IRequest<FileContentModel>
    {
        public WorkerScheduleTaskFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
