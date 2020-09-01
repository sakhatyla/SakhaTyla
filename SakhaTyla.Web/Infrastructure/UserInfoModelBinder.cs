using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Web.Infrastructure
{
    public class UserInfoModelBinder : IModelBinder
    {
        private readonly IUserInfoProvider _userInfoProvider;

        public UserInfoModelBinder(IUserInfoProvider userInfoProvider)
        {
            _userInfoProvider = userInfoProvider;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Cannot update instances");

            bindingContext.Result = ModelBindingResult.Success(await _userInfoProvider.GetUserInfoAsync());
        }
    }
}
