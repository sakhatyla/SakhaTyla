using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Menus;
using SakhaTyla.Core.Requests.Menus.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuModel>();
            CreateMap<Menu, MenuShortModel>();
            CreateMap<CreateMenu, Menu>();
            CreateMap<UpdateMenu, Menu>();
        }
    }
}
