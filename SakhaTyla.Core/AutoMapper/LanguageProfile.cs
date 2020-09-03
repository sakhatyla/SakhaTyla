using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Languages;
using SakhaTyla.Core.Requests.Languages.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<Language, LanguageModel>();
            CreateMap<Language, LanguageShortModel>();
            CreateMap<CreateLanguage, Language>();
            CreateMap<UpdateLanguage, Language>();
        }
    }
}
