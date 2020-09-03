using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Articles;
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleModel>();
            CreateMap<Article, ArticleShortModel>();
            CreateMap<CreateArticle, Article>()
                .ForMember(dest => dest.Title, o => o.MapFrom(src => src.Title.Trim().ToLower()))
                .ForMember(dest => dest.Text, o => o.MapFrom(src => src.TextSource.ProcessText()));
            CreateMap<UpdateArticle, Article>()
                .ForMember(dest => dest.Title, o => o.MapFrom(src => src.Title.Trim().ToLower()))
                .ForMember(dest => dest.Text, o => o.MapFrom(src => src.TextSource.ProcessText()));
        }
    }
}
