using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Pages;
using SakhaTyla.Core.Requests.Pages.Models;
using SakhaTyla.Web.Protos.Pages;

namespace SakhaTyla.Web.AutoMapper
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<CreatePageRequest, CreatePage>()
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.TypeOneOfCase == CreatePageRequest.TypeOneOfOneofCase.Type))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreatePageRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.ShortName, opt => opt.Condition(src => src.ShortNameOneOfCase == CreatePageRequest.ShortNameOneOfOneofCase.ShortName))
                .ForMember(dest => dest.ParentId, opt => opt.Condition(src => src.ParentIdOneOfCase == CreatePageRequest.ParentIdOneOfOneofCase.ParentId))
                .ForMember(dest => dest.Header, opt => opt.Condition(src => src.HeaderOneOfCase == CreatePageRequest.HeaderOneOfOneofCase.Header))
                .ForMember(dest => dest.Body, opt => opt.Condition(src => src.BodyOneOfCase == CreatePageRequest.BodyOneOfOneofCase.Body))
                .ForMember(dest => dest.MetaTitle, opt => opt.Condition(src => src.MetaTitleOneOfCase == CreatePageRequest.MetaTitleOneOfOneofCase.MetaTitle))
                .ForMember(dest => dest.MetaKeywords, opt => opt.Condition(src => src.MetaKeywordsOneOfCase == CreatePageRequest.MetaKeywordsOneOfOneofCase.MetaKeywords))
                .ForMember(dest => dest.MetaDescription, opt => opt.Condition(src => src.MetaDescriptionOneOfCase == CreatePageRequest.MetaDescriptionOneOfOneofCase.MetaDescription))
                .ForMember(dest => dest.ImageId, opt => opt.Condition(src => src.ImageIdOneOfCase == CreatePageRequest.ImageIdOneOfOneofCase.ImageId))
                .ForMember(dest => dest.Preview, opt => opt.Condition(src => src.PreviewOneOfCase == CreatePageRequest.PreviewOneOfOneofCase.Preview));
            CreateMap<DeletePageRequest, DeletePage>();
            CreateMap<GetPageRequest, GetPage>();
            CreateMap<GetPagesRequest, GetPages>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetPagesRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetPagesRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetPagesRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdatePageRequest, UpdatePage>()
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.TypeOneOfCase == UpdatePageRequest.TypeOneOfOneofCase.Type))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdatePageRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.ShortName, opt => opt.Condition(src => src.ShortNameOneOfCase == UpdatePageRequest.ShortNameOneOfOneofCase.ShortName))
                .ForMember(dest => dest.ParentId, opt => opt.Condition(src => src.ParentIdOneOfCase == UpdatePageRequest.ParentIdOneOfOneofCase.ParentId))
                .ForMember(dest => dest.Header, opt => opt.Condition(src => src.HeaderOneOfCase == UpdatePageRequest.HeaderOneOfOneofCase.Header))
                .ForMember(dest => dest.Body, opt => opt.Condition(src => src.BodyOneOfCase == UpdatePageRequest.BodyOneOfOneofCase.Body))
                .ForMember(dest => dest.MetaTitle, opt => opt.Condition(src => src.MetaTitleOneOfCase == UpdatePageRequest.MetaTitleOneOfOneofCase.MetaTitle))
                .ForMember(dest => dest.MetaKeywords, opt => opt.Condition(src => src.MetaKeywordsOneOfCase == UpdatePageRequest.MetaKeywordsOneOfOneofCase.MetaKeywords))
                .ForMember(dest => dest.MetaDescription, opt => opt.Condition(src => src.MetaDescriptionOneOfCase == UpdatePageRequest.MetaDescriptionOneOfOneofCase.MetaDescription))
                .ForMember(dest => dest.ImageId, opt => opt.Condition(src => src.ImageIdOneOfCase == UpdatePageRequest.ImageIdOneOfOneofCase.ImageId))
                .ForMember(dest => dest.Preview, opt => opt.Condition(src => src.PreviewOneOfCase == UpdatePageRequest.PreviewOneOfOneofCase.Preview));

            CreateMap<PageModel, Page>()
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type != default))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.ShortName, opt => opt.Condition(src => src.ShortName != default))
                .ForMember(dest => dest.ParentId, opt => opt.Condition(src => src.ParentId != default))
                .ForMember(dest => dest.Header, opt => opt.Condition(src => src.Header != default))
                .ForMember(dest => dest.Body, opt => opt.Condition(src => src.Body != default))
                .ForMember(dest => dest.MetaTitle, opt => opt.Condition(src => src.MetaTitle != default))
                .ForMember(dest => dest.MetaKeywords, opt => opt.Condition(src => src.MetaKeywords != default))
                .ForMember(dest => dest.MetaDescription, opt => opt.Condition(src => src.MetaDescription != default))
                .ForMember(dest => dest.ImageId, opt => opt.Condition(src => src.ImageId != default))
                .ForMember(dest => dest.Preview, opt => opt.Condition(src => src.Preview != default))
                .ForMember(dest => dest.CommentContainerId, opt => opt.Condition(src => src.CommentContainerId != default));
            CreateMap<PageModel<PageModel>, PagePageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Page>>(src.PageItems)));
        }
    }
}
