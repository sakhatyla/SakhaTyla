using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Comments;
using SakhaTyla.Core.Requests.Comments.Models;
using SakhaTyla.Web.Protos.Comments;

namespace SakhaTyla.Web.AutoMapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateCommentRequest, CreateComment>()
                .ForMember(dest => dest.ContainerId, opt => opt.Condition(src => src.ContainerIdOneOfCase == CreateCommentRequest.ContainerIdOneOfOneofCase.ContainerId))
                .ForMember(dest => dest.TextSource, opt => opt.Condition(src => src.TextSourceOneOfCase == CreateCommentRequest.TextSourceOneOfOneofCase.TextSource))
                .ForMember(dest => dest.AuthorId, opt => opt.Condition(src => src.AuthorIdOneOfCase == CreateCommentRequest.AuthorIdOneOfOneofCase.AuthorId))
                .ForMember(dest => dest.ParentId, opt => opt.Condition(src => src.ParentIdOneOfCase == CreateCommentRequest.ParentIdOneOfOneofCase.ParentId));
            CreateMap<DeleteCommentRequest, DeleteComment>();
            CreateMap<GetCommentRequest, GetComment>();
            CreateMap<GetCommentsRequest, GetComments>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetCommentsRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetCommentsRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetCommentsRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateCommentRequest, UpdateComment>()
                .ForMember(dest => dest.TextSource, opt => opt.Condition(src => src.TextSourceOneOfCase == UpdateCommentRequest.TextSourceOneOfOneofCase.TextSource));

            CreateMap<CommentModel, Comment>()
                .ForMember(dest => dest.ContainerId, opt => opt.Condition(src => src.ContainerId != default))
                .ForMember(dest => dest.Text, opt => opt.Condition(src => src.Text != default))
                .ForMember(dest => dest.TextSource, opt => opt.Condition(src => src.TextSource != default))
                .ForMember(dest => dest.AuthorId, opt => opt.Condition(src => src.AuthorId != default))
                .ForMember(dest => dest.ParentId, opt => opt.Condition(src => src.ParentId != default));
            CreateMap<PageModel<CommentModel>, CommentPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Comment>>(src.PageItems)));
        }
    }
}
