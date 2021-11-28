using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.WorkerRuns;
using SakhaTyla.Core.Requests.WorkerRuns.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class WorkerRunProfile : Profile
    {
        public WorkerRunProfile()
        {
            CreateMap<WorkerRun, WorkerRunModel>();
            CreateMap<WorkerRun, WorkerRunShortModel>();
            CreateMap<CreateWorkerRun, WorkerRun>();
        }
    }
}
