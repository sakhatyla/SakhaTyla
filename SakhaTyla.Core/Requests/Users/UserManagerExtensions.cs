using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Users
{
    public static class UserManagerExtensions
    {
        public static async Task UpdateUserEmail(this UserManager<User> userManager, User user, string email)
        {
            const string collisionError = "This email is already taken by other user";
            var emailCollision = await userManager.FindByEmailAsync(email);
            if (emailCollision != null) throw new ServiceException(collisionError);
            var userNameCollision = await userManager.FindByNameAsync(email);
            if (userNameCollision != null) throw new ServiceException(collisionError);
            var emailToken = await userManager.GenerateChangeEmailTokenAsync(user, email);
            var result = await userManager.ChangeEmailAsync(user, email, emailToken);
            result.CheckIfSucceeded();
            await userManager.ConfirmEmailAsync(user, emailToken);
            user.UserName = user.Email;
            await userManager.UpdateAsync(user);
            await userManager.UpdateNormalizedUserNameAsync(user);
        }
    }
}
