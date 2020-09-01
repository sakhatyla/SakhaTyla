using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Cynosura.Core.Services;

namespace SakhaTyla.Core.Requests
{
    public static class IdentityExtensions
    {
        public static void CheckIfSucceeded(this IdentityResult result)
        {
            if (result.Succeeded)
                return;

            var errorDescription = result.Errors.Aggregate("",
                (current, error) => current + error.Description + " \r\n ");
            throw new ServiceException($"{errorDescription}");
        }
    }
}
