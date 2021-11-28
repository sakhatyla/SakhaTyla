using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Web.Infrastructure
{
    public class MyProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;

        public MyProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            //get claims from ClaimsPrincipal 
            var claims = context.Subject.FindAll(c =>
            {
                return c.Type == JwtClaimTypes.Role || c.Type == JwtClaimTypes.Name;
            }).ToList();

            claims.Add(new Claim("first_name", user.FirstName ?? ""));
            claims.Add(new Claim("last_name", user.LastName ?? ""));

            //add your claims 
            context.IssuedClaims.AddRange(claims);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            // await base.IsActiveAsync(context);
            return Task.CompletedTask;
        }
    }
}
