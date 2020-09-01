using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data
{
    public class CustomRoleStore : RoleStore<Role, DataContext, int>
    {
        public CustomRoleStore(DataContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        public override async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            role.CreationDate = role.ModificationDate = DateTime.UtcNow;
            return await base.CreateAsync(role, cancellationToken);
        }
        
        public override async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            role.ModificationDate = DateTime.UtcNow;
            return await base.UpdateAsync(role, cancellationToken);
        }
    }
}
