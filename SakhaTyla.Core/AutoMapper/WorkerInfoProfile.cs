using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.WorkerInfos;
using SakhaTyla.Core.Requests.WorkerInfos.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class WorkerInfoProfile : Profile
    {
        public WorkerInfoProfile()
        {
            CreateMap<WorkerInfo, WorkerInfoModel>();
            CreateMap<WorkerInfo, WorkerInfoShortModel>();
            CreateMap<CreateWorkerInfo, WorkerInfo>();
            CreateMap<UpdateWorkerInfo, WorkerInfo>();
        }
    }
}
