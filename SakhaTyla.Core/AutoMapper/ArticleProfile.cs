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
            CreateMap<CreateArticle, Article>();
            CreateMap<UpdateArticle, Article>();
        }
    }
}
