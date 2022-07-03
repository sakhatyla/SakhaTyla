using SakhaTyla.Core.Security;

namespace SakhaTyla.Web.Front.Infrastructure
{
    public class UserInfoProvider : IUserInfoProvider
    {
        public Task<UserInfoModel> GetUserInfoAsync()
        {
            return Task.FromResult<UserInfoModel>(new UserInfoModel());
        }
    }
}
