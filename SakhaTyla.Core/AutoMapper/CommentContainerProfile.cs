using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.CommentContainers;
using SakhaTyla.Core.Requests.CommentContainers.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class CommentContainerProfile : Profile
    {
        public CommentContainerProfile()
        {
            CreateMap<CommentContainer, CommentContainerModel>();
            CreateMap<CommentContainer, CommentContainerShortModel>();
        }
    }
}
