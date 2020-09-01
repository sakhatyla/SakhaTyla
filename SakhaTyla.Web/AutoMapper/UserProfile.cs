using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Users;
using SakhaTyla.Core.Requests.Users.Models;
using SakhaTyla.Web.Protos.Users;

namespace SakhaTyla.Web.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, CreateUser>()
                .ForMember(dest => dest.Email, opt => opt.Condition(src => src.EmailOneOfCase == CreateUserRequest.EmailOneOfOneofCase.Email))
                .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstNameOneOfCase == CreateUserRequest.FirstNameOneOfOneofCase.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastNameOneOfCase == CreateUserRequest.LastNameOneOfOneofCase.LastName));
            CreateMap<DeleteUserRequest, DeleteUser>();
            CreateMap<GetUserRequest, GetUser>();
            CreateMap<GetUsersRequest, GetUsers>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetUsersRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetUsersRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetUsersRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateUserRequest, UpdateUser>()
                .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstNameOneOfCase == UpdateUserRequest.FirstNameOneOfOneofCase.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastNameOneOfCase == UpdateUserRequest.LastNameOneOfOneofCase.LastName));

            CreateMap<UserModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.Condition(src => src.UserName != default))
                .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != default))
                .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstName != default))
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastName != default));
            CreateMap<PageModel<UserModel>, UserPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<User>>(src.PageItems)));
        }
    }
}
