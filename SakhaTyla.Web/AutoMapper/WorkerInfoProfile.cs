using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.WorkerInfos;
using SakhaTyla.Core.Requests.WorkerInfos.Models;
using SakhaTyla.Web.Protos.WorkerInfos;

namespace SakhaTyla.Web.AutoMapper
{
    public class WorkerInfoProfile : Profile
    {
        public WorkerInfoProfile()
        {
            CreateMap<CreateWorkerInfoRequest, CreateWorkerInfo>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateWorkerInfoRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.ClassName, opt => opt.Condition(src => src.ClassNameOneOfCase == CreateWorkerInfoRequest.ClassNameOneOfOneofCase.ClassName));
            CreateMap<DeleteWorkerInfoRequest, DeleteWorkerInfo>();
            CreateMap<GetWorkerInfoRequest, GetWorkerInfo>();
            CreateMap<GetWorkerInfosRequest, GetWorkerInfos>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetWorkerInfosRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetWorkerInfosRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetWorkerInfosRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateWorkerInfoRequest, UpdateWorkerInfo>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateWorkerInfoRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.ClassName, opt => opt.Condition(src => src.ClassNameOneOfCase == UpdateWorkerInfoRequest.ClassNameOneOfOneofCase.ClassName));

            CreateMap<WorkerInfoModel, WorkerInfo>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.ClassName, opt => opt.Condition(src => src.ClassName != default));
            CreateMap<PageModel<WorkerInfoModel>, WorkerInfoPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<WorkerInfo>>(src.PageItems)));
        }
    }
}
