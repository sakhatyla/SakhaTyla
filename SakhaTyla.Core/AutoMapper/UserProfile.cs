using System.Linq;
using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Users;
using SakhaTyla.Core.Requests.Users.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(dst => dst.RoleIds, opt => opt.MapFrom(src => src.Roles.Select(e => e.Id)));
            CreateMap<User, UserShortModel>();
            CreateMap<CreateUser, User>();
            CreateMap<UpdateUser, User>();
        }
    }
}
