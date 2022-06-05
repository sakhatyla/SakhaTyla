using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Comments;
using SakhaTyla.Core.Requests.Comments.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentModel>();
            CreateMap<Comment, CommentShortModel>();
            CreateMap<CreateComment, Comment>()
                .ForMember(dest => dest.Text, o => o.MapFrom(src => src.TextSource!.ProcessCommentText()));
            CreateMap<UpdateComment, Comment>()
                .ForMember(dest => dest.Text, o => o.MapFrom(src => src.TextSource!.ProcessCommentText()));
        }
    }
}
