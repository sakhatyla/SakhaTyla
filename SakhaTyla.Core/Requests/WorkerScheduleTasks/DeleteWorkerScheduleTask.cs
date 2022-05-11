using System;
using MediatR;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public class DeleteWorkerScheduleTask : IRequest
    {
        public int Id { get; set; }
    }
}
