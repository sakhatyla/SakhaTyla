using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace SakhaTyla.Web.Infrastructure
{
    public class MyProfileService : IProfileService
    {
        public MyProfileService()
        { 
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //get role claims from ClaimsPrincipal 
            var roleClaims = context.Subject.FindAll(c => 
            {
                return c.Type == JwtClaimTypes.Role || c.Type == JwtClaimTypes.Name;
            });

            //add your role claims 
            context.IssuedClaims.AddRange(roleClaims);
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            // await base.IsActiveAsync(context);
            return Task.CompletedTask;
        }
    }
}
