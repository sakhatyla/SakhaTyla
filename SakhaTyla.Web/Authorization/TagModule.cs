using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Cynosura.Web.Authorization;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Web.Authorization
{
    public class TagModule : IPolicyModule
    {
        public void RegisterPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("ReadTag",
                policy => policy.RequireClaim(ClaimTypes.Role, RoleConfig.Administrator, RoleConfig.Editor));
            options.AddPolicy("WriteTag",
                policy => policy.RequireClaim(ClaimTypes.Role, RoleConfig.Administrator));
        }
    }
}
