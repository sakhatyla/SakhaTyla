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

        public UserMigration(SourceLoader sourceLoader, 
            IMediator mediator,
            IEntityRepository<Role> roleRepository)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
            _roleRepository = roleRepository;
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
                await _mediator.Send(createUser);
            }
        }
    }
}
