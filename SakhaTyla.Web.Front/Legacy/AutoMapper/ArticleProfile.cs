using AutoMapper;
using SakhaTyla.Web.Front.Legacy.Models;

namespace SakhaTyla.Web.Front.Legacy.AutoMapper
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Core.Requests.Public.Articles.Models.ArticleModel, ArticleModel>();
            CreateMap<Core.Requests.Public.Articles.Models.ArticleGroupModel, ArticleGroupModel>();
            CreateMap<Core.Requests.Public.Articles.Models.TranslateModel, TranslateModel>();
        }
    }
}
