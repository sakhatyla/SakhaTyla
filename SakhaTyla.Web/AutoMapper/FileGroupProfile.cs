using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.FileGroups;
using SakhaTyla.Core.Requests.FileGroups.Models;
using SakhaTyla.Web.Protos.FileGroups;

namespace SakhaTyla.Web.AutoMapper
{
    public class FileGroupProfile : Profile
    {
        public FileGroupProfile()
        {
            CreateMap<CreateFileGroupRequest, CreateFileGroup>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateFileGroupRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.TypeOneOfCase == CreateFileGroupRequest.TypeOneOfOneofCase.Type))
                .ForMember(dest => dest.Location, opt => opt.Condition(src => src.LocationOneOfCase == CreateFileGroupRequest.LocationOneOfOneofCase.Location))
                .ForMember(dest => dest.Accept, opt => opt.Condition(src => src.AcceptOneOfCase == CreateFileGroupRequest.AcceptOneOfOneofCase.Accept));
            CreateMap<DeleteFileGroupRequest, DeleteFileGroup>();
            CreateMap<GetFileGroupRequest, GetFileGroup>();
            CreateMap<GetFileGroupsRequest, GetFileGroups>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetFileGroupsRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetFileGroupsRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetFileGroupsRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateFileGroupRequest, UpdateFileGroup>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateFileGroupRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.TypeOneOfCase == UpdateFileGroupRequest.TypeOneOfOneofCase.Type))
                .ForMember(dest => dest.Location, opt => opt.Condition(src => src.LocationOneOfCase == UpdateFileGroupRequest.LocationOneOfOneofCase.Location))
                .ForMember(dest => dest.Accept, opt => opt.Condition(src => src.AcceptOneOfCase == UpdateFileGroupRequest.AcceptOneOfOneofCase.Accept));

            CreateMap<FileGroupModel, FileGroup>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type != default))
                .ForMember(dest => dest.Location, opt => opt.Condition(src => src.Location != default))
                .ForMember(dest => dest.Accept, opt => opt.Condition(src => src.Accept != default));
            CreateMap<PageModel<FileGroupModel>, FileGroupPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<FileGroup>>(src.PageItems)));
        }
    }
}
