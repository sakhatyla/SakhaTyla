using System;
using MediatR;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public class GetWorkerScheduleTask : IRequest<WorkerScheduleTaskModel?>
    {
        public int Id { get; set; }
    }
}
