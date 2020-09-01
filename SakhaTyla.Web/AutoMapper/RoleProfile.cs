using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Roles;
using SakhaTyla.Core.Requests.Roles.Models;
using SakhaTyla.Web.Protos.Roles;

namespace SakhaTyla.Web.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleRequest, CreateRole>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateRoleRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.DisplayName, opt => opt.Condition(src => src.DisplayNameOneOfCase == CreateRoleRequest.DisplayNameOneOfOneofCase.DisplayName));
            CreateMap<DeleteRoleRequest, DeleteRole>();
            CreateMap<GetRoleRequest, GetRole>();
            CreateMap<GetRolesRequest, GetRoles>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetRolesRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetRolesRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetRolesRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateRoleRequest, UpdateRole>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateRoleRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.DisplayName, opt => opt.Condition(src => src.DisplayNameOneOfCase == UpdateRoleRequest.DisplayNameOneOfOneofCase.DisplayName));

            CreateMap<RoleModel, Role>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.DisplayName, opt => opt.Condition(src => src.DisplayName != default));
            CreateMap<PageModel<RoleModel>, RolePageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Role>>(src.PageItems)));
        }
    }
}
