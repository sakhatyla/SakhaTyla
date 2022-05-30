using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Pages;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<Page, PageModel>();
            CreateMap<Page, PageShortModel>();
            CreateMap<CreatePage, Page>();
            CreateMap<UpdatePage, Page>();
        }
    }
}
