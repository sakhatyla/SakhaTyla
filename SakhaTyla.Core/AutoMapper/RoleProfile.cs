using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Roles;
using SakhaTyla.Core.Requests.Roles.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleModel>();
            CreateMap<Role, RoleShortModel>();
            CreateMap<CreateRole, Role>();
            CreateMap<UpdateRole, Role>();
        }
    }
}
