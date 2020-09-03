using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Cynosura.Web.Authorization;

namespace SakhaTyla.Web.Authorization
{
    public class CategoryModule : IPolicyModule
    {
        public void RegisterPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("ReadCategory",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
            options.AddPolicy("WriteCategory",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
        }
    }
}