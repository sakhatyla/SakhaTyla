using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Languages;
using SakhaTyla.Core.Requests.Languages.Models;
using SakhaTyla.Web.Protos.Languages;

namespace SakhaTyla.Web.AutoMapper
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<CreateLanguageRequest, CreateLanguage>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateLanguageRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Code, opt => opt.Condition(src => src.CodeOneOfCase == CreateLanguageRequest.CodeOneOfOneofCase.Code));
            CreateMap<DeleteLanguageRequest, DeleteLanguage>();
            CreateMap<GetLanguageRequest, GetLanguage>();
            CreateMap<GetLanguagesRequest, GetLanguages>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetLanguagesRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetLanguagesRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetLanguagesRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateLanguageRequest, UpdateLanguage>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateLanguageRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Code, opt => opt.Condition(src => src.CodeOneOfCase == UpdateLanguageRequest.CodeOneOfOneofCase.Code));

            CreateMap<LanguageModel, Language>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.Code, opt => opt.Condition(src => src.Code != default));
            CreateMap<PageModel<LanguageModel>, LanguagePageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Language>>(src.PageItems)));
        }
    }
}
