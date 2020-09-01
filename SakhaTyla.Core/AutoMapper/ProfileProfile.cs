using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Profile;
using SakhaTyla.Core.Requests.Profile.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<User, ProfileModel>();
            CreateMap<UpdateProfile, User>();
        }
    }
}
