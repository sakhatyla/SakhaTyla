using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.MenuItems;
using SakhaTyla.Core.Requests.MenuItems.Models;
using SakhaTyla.Web.Protos.MenuItems;

namespace SakhaTyla.Web.AutoMapper
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<CreateMenuItemRequest, CreateMenuItem>()
                .ForMember(dest => dest.MenuId, opt => opt.Condition(src => src.MenuIdOneOfCase == CreateMenuItemRequest.MenuIdOneOfOneofCase.MenuId))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateMenuItemRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Url, opt => opt.Condition(src => src.UrlOneOfCase == CreateMenuItemRequest.UrlOneOfOneofCase.Url))
                .ForMember(dest => dest.ParentId, opt => opt.Condition(src => src.ParentIdOneOfCase == CreateMenuItemRequest.ParentIdOneOfOneofCase.ParentId));
            CreateMap<DeleteMenuItemRequest, DeleteMenuItem>();
            CreateMap<GetMenuItemRequest, GetMenuItem>();
            CreateMap<GetMenuItemsRequest, GetMenuItems>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetMenuItemsRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetMenuItemsRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetMenuItemsRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateMenuItemRequest, UpdateMenuItem>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateMenuItemRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Url, opt => opt.Condition(src => src.UrlOneOfCase == UpdateMenuItemRequest.UrlOneOfOneofCase.Url))
                .ForMember(dest => dest.ParentId, opt => opt.Condition(src => src.ParentIdOneOfCase == UpdateMenuItemRequest.ParentIdOneOfOneofCase.ParentId));

            CreateMap<MenuItemModel, MenuItem>()
                .ForMember(dest => dest.MenuId, opt => opt.Condition(src => src.MenuId != default))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.Url, opt => opt.Condition(src => src.Url != default))
                .ForMember(dest => dest.Weight, opt => opt.Condition(src => src.Weight != default))
                .ForMember(dest => dest.ParentId, opt => opt.Condition(src => src.ParentId != default));
            CreateMap<PageModel<MenuItemModel>, MenuItemPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<MenuItem>>(src.PageItems)));
        }
    }
}
