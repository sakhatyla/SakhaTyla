using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Menus;
using SakhaTyla.Core.Requests.Menus.Models;
using SakhaTyla.Web.Protos.Menus;

namespace SakhaTyla.Web.AutoMapper
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<CreateMenuRequest, CreateMenu>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateMenuRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Code, opt => opt.Condition(src => src.CodeOneOfCase == CreateMenuRequest.CodeOneOfOneofCase.Code));
            CreateMap<DeleteMenuRequest, DeleteMenu>();
            CreateMap<GetMenuRequest, GetMenu>();
            CreateMap<GetMenusRequest, GetMenus>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetMenusRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetMenusRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetMenusRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateMenuRequest, UpdateMenu>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateMenuRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Code, opt => opt.Condition(src => src.CodeOneOfCase == UpdateMenuRequest.CodeOneOfOneofCase.Code));

            CreateMap<MenuModel, Menu>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.Code, opt => opt.Condition(src => src.Code != default));
            CreateMap<PageModel<MenuModel>, MenuPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Menu>>(src.PageItems)));
        }
    }
}
