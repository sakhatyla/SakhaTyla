using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly DataContext _dataContext;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public DatabaseInitializer(DataContext dataContext, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.MigrateAsync();

            var administratorRoleName = "Administrator";
            if (await _roleManager.FindByNameAsync(administratorRoleName) == null)
            {
                await _roleManager.CreateAsync(new Role() 
                { 
                    Name = administratorRoleName,
                    DisplayName = administratorRoleName,
                });
            }

            var administratorEmail = "admin@cynosura.dev";
            if (!(await _userManager.Users.AnyAsync()))
            {
                var user = new User()
                {
                    UserName = administratorEmail,
                    Email = administratorEmail,
                    FirstName = "Administrator",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, administratorRoleName);
                await _userManager.AddPasswordAsync(user, "Admin123!");
            }
        }
    }
}
