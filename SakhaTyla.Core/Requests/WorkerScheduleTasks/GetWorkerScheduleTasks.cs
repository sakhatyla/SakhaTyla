using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public class GetWorkerScheduleTasks : IRequest<PageModel<WorkerScheduleTaskModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public WorkerScheduleTaskFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
