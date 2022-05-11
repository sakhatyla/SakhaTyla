using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.WorkerScheduleTasks;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class WorkerScheduleTaskProfile : Profile
    {
        public WorkerScheduleTaskProfile()
        {
            CreateMap<WorkerScheduleTask, WorkerScheduleTaskModel>();
            CreateMap<WorkerScheduleTask, WorkerScheduleTaskShortModel>();
            CreateMap<CreateWorkerScheduleTask, WorkerScheduleTask>();
            CreateMap<UpdateWorkerScheduleTask, WorkerScheduleTask>();
        }
    }
}
