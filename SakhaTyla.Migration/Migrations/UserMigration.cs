using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Migration.SourceDatabase;
using SakhaTyla.Core.Requests.Users;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Migration.Migrations
{
    class UserMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;
        private readonly IEntityRepository<Role> _roleRepository;
        private readonly IEntityRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserMigration(SourceLoader sourceLoader, 
            IMediator mediator,
            IEntityRepository<Role> roleRepository,
            IEntityRepository<User> userRepository,
            IUnitOfWork unitOfWork)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task MigrateUsers()
        {
            var roles = await _roleRepository.GetEntities()
                .ToListAsync();

            var userRoles = await _sourceLoader.GetUserRolesAsync();

            // TODO: migrate user logins
            var users = await _sourceLoader.GetUsersAsync();
            foreach (var user in users)
            {
                var createUser = new CreateUser()
                {
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.Name,
                    RoleIds = userRoles.Where(e => e.UserId == user.Id)
                        .Select(e => roles.First(r => r.Name == e.RoleName).Id)
                        .ToList(),
                };
                var createdUser = await _mediator.Send(createUser);

                if (user.PasswordHash != null)
                {
                    var newUser = await _userRepository.GetEntities()
                        .Where(e => e.Id == createdUser.Id)
                        .FirstAsync();
                    newUser.PasswordHash = user.PasswordHash;
                    await _unitOfWork.CommitAsync();
                }
            }
        }
    }
}
