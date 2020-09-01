using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Profile.Models;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Core.Requests.Profile
{
    public class GetProfileHandler : IRequestHandler<GetProfile, ProfileModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserInfoProvider _userInfoProvider;

        public GetProfileHandler(UserManager<User> userManager,
            IMapper mapper,
            IUserInfoProvider userInfoProvider)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userInfoProvider = userInfoProvider;
        }

        public async Task<ProfileModel> Handle(GetProfile request, CancellationToken cancellationToken)
        {
            var userInfo = await _userInfoProvider.GetUserInfoAsync();
            var user = await _userManager.Users.FirstOrDefaultAsync(e => e.Id == userInfo.UserId, cancellationToken);
            var model = _mapper.Map<User, ProfileModel>(user);

            return model;
        }
    }
}
