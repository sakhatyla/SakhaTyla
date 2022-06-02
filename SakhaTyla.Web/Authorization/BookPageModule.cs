using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Cynosura.Web.Authorization;

namespace SakhaTyla.Web.Authorization
{
    public class BookPageModule : IPolicyModule
    {
        public void RegisterPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("ReadBookPage",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
            options.AddPolicy("WriteBookPage",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
        }
    }
}