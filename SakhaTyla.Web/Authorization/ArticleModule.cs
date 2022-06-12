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
    public class ArticleModule : IPolicyModule
    {
        public void RegisterPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("ReadArticle",
                policy => policy.RequireClaim(ClaimTypes.Role, RoleConfig.Administrator, RoleConfig.Editor));
            options.AddPolicy("WriteArticle",
                policy => policy.RequireClaim(ClaimTypes.Role, RoleConfig.Administrator, RoleConfig.Editor));
        }
    }
}
