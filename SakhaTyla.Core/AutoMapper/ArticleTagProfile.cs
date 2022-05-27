using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.ArticleTags;
using SakhaTyla.Core.Requests.ArticleTags.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class ArticleTagProfile : Profile
    {
        public ArticleTagProfile()
        {
            CreateMap<ArticleTag, ArticleTagModel>();
            CreateMap<int, ArticleTag>()
                .ConvertUsing((source, destination) =>
                {
                    if (destination == null)
                    {
                        destination = new ArticleTag();
                    }
                    destination.TagId = source;
                    return destination;
                }); ;
        }
    }
}
