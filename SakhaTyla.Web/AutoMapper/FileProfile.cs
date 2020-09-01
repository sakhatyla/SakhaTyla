using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Files;
using SakhaTyla.Core.Requests.Files.Models;
using SakhaTyla.Web.Protos.Files;

namespace SakhaTyla.Web.AutoMapper
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<CreateFileRequest, CreateFile>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateFileRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.ContentType, opt => opt.Condition(src => src.ContentTypeOneOfCase == CreateFileRequest.ContentTypeOneOfOneofCase.ContentType))
                .ForMember(dest => dest.Content, opt => opt.Condition(src => src.ContentOneOfCase == CreateFileRequest.ContentOneOfOneofCase.Content))
                .ForMember(dest => dest.GroupId, opt => opt.Condition(src => src.GroupIdOneOfCase == CreateFileRequest.GroupIdOneOfOneofCase.GroupId));
            CreateMap<DeleteFileRequest, DeleteFile>();
            CreateMap<GetFileRequest, GetFile>();
            CreateMap<GetFilesRequest, GetFiles>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetFilesRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetFilesRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetFilesRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateFileRequest, UpdateFile>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateFileRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.ContentType, opt => opt.Condition(src => src.ContentTypeOneOfCase == UpdateFileRequest.ContentTypeOneOfOneofCase.ContentType))
                .ForMember(dest => dest.Content, opt => opt.Condition(src => src.ContentOneOfCase == UpdateFileRequest.ContentOneOfOneofCase.Content));

            CreateMap<FileModel, File>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.ContentType, opt => opt.Condition(src => src.ContentType != default))
                .ForMember(dest => dest.Url, opt => opt.Condition(src => src.Url != default))
                .ForMember(dest => dest.GroupId, opt => opt.Condition(src => src.GroupId != default));
            CreateMap<PageModel<FileModel>, FilePageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<File>>(src.PageItems)));
        }
    }
}
