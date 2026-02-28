using System.Threading.Tasks;
using Cynosura.Core.Data;
using Cynosura.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly DataContext _dataContext;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IEntityRepository<FileGroup> _fileGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DatabaseInitializer(DataContext dataContext, RoleManager<Role> roleManager,
            UserManager<User> userManager,
            IEntityRepository<FileGroup> fileGroupRepository,
            IUnitOfWork unitOfWork)
        {
            _dataContext = dataContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _fileGroupRepository = fileGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.MigrateAsync();

            var roles = GetRoles();
            foreach (var role in roles)
            {
                if (await _roleManager.FindByNameAsync(role.Name) == null)
                {
                    await _roleManager.CreateAsync(new Role(role.DisplayName)
                    {
                        Name = role.Name,
                    });
                }
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
                await _userManager.AddToRoleAsync(user, RoleConfig.Administrator);
                await _userManager.AddPasswordAsync(user, "Admin123!");
            }

            var fileGroups = GetFileGroups();
            foreach (var fileGroup in fileGroups)
            {
                if (!await _fileGroupRepository.GetEntities().AnyAsync(e => e.Name == fileGroup.Name))
                {
                    _fileGroupRepository.Add(fileGroup);
                }
            }

            await _unitOfWork.CommitAsync();
        }

        private static Role[] GetRoles()
        {
            return new[]
            {
                new Role("Administrator")
                {
                    Name = RoleConfig.Administrator,
                },
                new Role("Editor")
                {
                    Name = RoleConfig.Editor,
                },
            };
        }

        private static FileGroup[] GetFileGroups()
        {
            return new[]
            {
                new FileGroup("ImportFiles", Core.Enums.FileGroupType.Storage)
                {
                    Location = "importfiles",
                    Accept = ".xlsx",
                },
            };
        }
    }
}
