using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Migration.SourceDatabase;
using SakhaTyla.Core.Requests.Users;

namespace SakhaTyla.Migration.Migrations
{
    class UserMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;

        public UserMigration(SourceLoader sourceLoader, IMediator mediator)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
        }

        public async Task MigrateUsers()
        {
            // TODO: migrate user logins
            // TODO: migrate user roles
            var users = await _sourceLoader.GetUsersAsync();
            foreach (var user in users)
            {
                var createUser = new CreateUser()
                {
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.Name
                };
                await _mediator.Send(createUser);
            }
        }
    }
}
