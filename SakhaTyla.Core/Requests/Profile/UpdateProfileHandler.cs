using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Users;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Core.Requests.Profile
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfile>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserInfoProvider _userInfoProvider;
        private readonly IMapper _mapper;

        public UpdateProfileHandler(
            UserManager<User> userManager,
            IUserInfoProvider userInfoProvider,
            IMapper mapper)
        {
            _userManager = userManager;
            _userInfoProvider = userInfoProvider;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProfile request, CancellationToken cancellationToken)
        {
            var userInfo = await _userInfoProvider.GetUserInfoAsync();
            var user = await _userManager.Users
                .FirstOrDefaultAsync(e => e.Id == userInfo.UserId, cancellationToken);
            _mapper.Map(request, user);

            var result = await _userManager.UpdateAsync(user);
            result.CheckIfSucceeded();
            return Unit.Value;
        }
    }
}
