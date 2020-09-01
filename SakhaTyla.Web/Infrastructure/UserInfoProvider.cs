using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Web.Infrastructure
{
    public class UserInfoProvider : IUserInfoProvider
    {
        private UserInfoModel _userInfoModel;
        private readonly IEntityRepository<User> _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoProvider(IEntityRepository<User> userRepository,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserInfoModel> GetUserInfoAsync()
        {
            if (_userInfoModel == null)
            {
                var context = _httpContextAccessor.HttpContext;
                var userName = context.User.Identity.Name;
                _userInfoModel = new UserInfoModel
                {
                    User = await _userRepository.GetEntities().FirstOrDefaultAsync(e => e.UserName == userName),
                };
                if (_userInfoModel.User != null)
                    _userInfoModel.Roles = await _userManager.GetRolesAsync(_userInfoModel.User);
            }
            return _userInfoModel;
        }
    }
}
