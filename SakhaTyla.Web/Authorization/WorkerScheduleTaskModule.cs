using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Cynosura.Web.Authorization;

namespace SakhaTyla.Web.Authorization
{
    public class WorkerScheduleTaskModule : IPolicyModule
    {
        public void RegisterPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("ReadWorkerScheduleTask",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
            options.AddPolicy("WriteWorkerScheduleTask",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
        }
    }
}