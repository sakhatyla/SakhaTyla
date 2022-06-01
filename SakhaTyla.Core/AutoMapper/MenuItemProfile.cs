using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.MenuItems;
using SakhaTyla.Core.Requests.MenuItems.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemModel>();
            CreateMap<MenuItem, MenuItemShortModel>();
            CreateMap<CreateMenuItem, MenuItem>();
            CreateMap<UpdateMenuItem, MenuItem>();
        }
    }
}
