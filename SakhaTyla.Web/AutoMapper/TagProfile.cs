using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Tags;
using SakhaTyla.Core.Requests.Tags.Models;
using SakhaTyla.Web.Protos.Tags;

namespace SakhaTyla.Web.AutoMapper
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<CreateTagRequest, CreateTag>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateTagRequest.NameOneOfOneofCase.Name));
            CreateMap<DeleteTagRequest, DeleteTag>();
            CreateMap<GetTagRequest, GetTag>();
            CreateMap<GetTagsRequest, GetTags>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetTagsRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetTagsRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetTagsRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateTagRequest, UpdateTag>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateTagRequest.NameOneOfOneofCase.Name));

            CreateMap<TagModel, Tag>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default));
            CreateMap<PageModel<TagModel>, TagPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Tag>>(src.PageItems)));
        }
    }
}
