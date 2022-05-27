using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Tags;
using SakhaTyla.Core.Requests.Tags.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagModel>();
            CreateMap<Tag, TagShortModel>();
            CreateMap<CreateTag, Tag>();
            CreateMap<UpdateTag, Tag>();
        }
    }
}
