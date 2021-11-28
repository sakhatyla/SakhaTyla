using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.WorkerRuns;
using SakhaTyla.Core.Requests.WorkerRuns.Models;
using SakhaTyla.Web.Protos.WorkerRuns;

namespace SakhaTyla.Web.AutoMapper
{
    public class WorkerRunProfile : Profile
    {
        public WorkerRunProfile()
        {
            CreateMap<CreateWorkerRunRequest, CreateWorkerRun>()
                .ForMember(dest => dest.WorkerInfoId, opt => opt.Condition(src => src.WorkerInfoIdOneOfCase == CreateWorkerRunRequest.WorkerInfoIdOneOfOneofCase.WorkerInfoId))
                .ForMember(dest => dest.Data, opt => opt.Condition(src => src.DataOneOfCase == CreateWorkerRunRequest.DataOneOfOneofCase.Data));
            CreateMap<DeleteWorkerRunRequest, DeleteWorkerRun>();
            CreateMap<GetWorkerRunRequest, GetWorkerRun>();
            CreateMap<GetWorkerRunsRequest, GetWorkerRuns>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetWorkerRunsRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetWorkerRunsRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetWorkerRunsRequest.OrderDirectionOneOfOneofCase.OrderDirection));

            CreateMap<WorkerRunModel, WorkerRun>()
                .ForMember(dest => dest.WorkerInfoId, opt => opt.Condition(src => src.WorkerInfoId != default))
                .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status != default))
                .ForMember(dest => dest.StartDateTime, opt => opt.Condition(src => src.StartDateTime != default))
                .ForMember(dest => dest.EndDateTime, opt => opt.Condition(src => src.EndDateTime != default))
                .ForMember(dest => dest.Data, opt => opt.Condition(src => src.Data != default))
                .ForMember(dest => dest.Result, opt => opt.Condition(src => src.Result != default))
                .ForMember(dest => dest.ResultData, opt => opt.Condition(src => src.ResultData != default));
            CreateMap<PageModel<WorkerRunModel>, WorkerRunPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<WorkerRun>>(src.PageItems)));
        }
    }
}
