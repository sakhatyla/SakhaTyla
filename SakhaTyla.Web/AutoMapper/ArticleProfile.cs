using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Articles;
using SakhaTyla.Core.Requests.Articles.Models;
using SakhaTyla.Web.Protos.Articles;

namespace SakhaTyla.Web.AutoMapper
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<CreateArticleRequest, CreateArticle>()
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.TitleOneOfCase == CreateArticleRequest.TitleOneOfOneofCase.Title))
                .ForMember(dest => dest.TextSource, opt => opt.Condition(src => src.TextSourceOneOfCase == CreateArticleRequest.TextSourceOneOfOneofCase.TextSource))
                .ForMember(dest => dest.FromLanguageId, opt => opt.Condition(src => src.FromLanguageIdOneOfCase == CreateArticleRequest.FromLanguageIdOneOfOneofCase.FromLanguageId))
                .ForMember(dest => dest.ToLanguageId, opt => opt.Condition(src => src.ToLanguageIdOneOfCase == CreateArticleRequest.ToLanguageIdOneOfOneofCase.ToLanguageId))
                .ForMember(dest => dest.Fuzzy, opt => opt.Condition(src => src.FuzzyOneOfCase == CreateArticleRequest.FuzzyOneOfOneofCase.Fuzzy))
                .ForMember(dest => dest.CategoryId, opt => opt.Condition(src => src.CategoryIdOneOfCase == CreateArticleRequest.CategoryIdOneOfOneofCase.CategoryId));
            CreateMap<DeleteArticleRequest, DeleteArticle>();
            CreateMap<GetArticleRequest, GetArticle>();
            CreateMap<GetArticlesRequest, GetArticles>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetArticlesRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetArticlesRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetArticlesRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateArticleRequest, UpdateArticle>()
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.TitleOneOfCase == UpdateArticleRequest.TitleOneOfOneofCase.Title))
                .ForMember(dest => dest.TextSource, opt => opt.Condition(src => src.TextSourceOneOfCase == UpdateArticleRequest.TextSourceOneOfOneofCase.TextSource))
                .ForMember(dest => dest.FromLanguageId, opt => opt.Condition(src => src.FromLanguageIdOneOfCase == UpdateArticleRequest.FromLanguageIdOneOfOneofCase.FromLanguageId))
                .ForMember(dest => dest.ToLanguageId, opt => opt.Condition(src => src.ToLanguageIdOneOfCase == UpdateArticleRequest.ToLanguageIdOneOfOneofCase.ToLanguageId))
                .ForMember(dest => dest.Fuzzy, opt => opt.Condition(src => src.FuzzyOneOfCase == UpdateArticleRequest.FuzzyOneOfOneofCase.Fuzzy))
                .ForMember(dest => dest.CategoryId, opt => opt.Condition(src => src.CategoryIdOneOfCase == UpdateArticleRequest.CategoryIdOneOfOneofCase.CategoryId));

            CreateMap<ArticleModel, Article>()
                .ForMember(dest => dest.Title, opt => opt.Condition(src => src.Title != default))
                .ForMember(dest => dest.Text, opt => opt.Condition(src => src.Text != default))
                .ForMember(dest => dest.TextSource, opt => opt.Condition(src => src.TextSource != default))
                .ForMember(dest => dest.FromLanguageId, opt => opt.Condition(src => src.FromLanguageId != default))
                .ForMember(dest => dest.ToLanguageId, opt => opt.Condition(src => src.ToLanguageId != default))
                .ForMember(dest => dest.Fuzzy, opt => opt.Condition(src => src.Fuzzy != default))
                .ForMember(dest => dest.CategoryId, opt => opt.Condition(src => src.CategoryId != default));
            CreateMap<PageModel<ArticleModel>, ArticlePageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Article>>(src.PageItems)));
        }
    }
}
