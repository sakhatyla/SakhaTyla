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
    public class CustomUserStore : UserStore<User, Role, DataContext, int>
    {
        public CustomUserStore(DataContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        public override async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            user.CreationDate = user.ModificationDate = DateTime.UtcNow;
            return await base.CreateAsync(user, cancellationToken);
        }
        
        public override async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            user.ModificationDate = DateTime.UtcNow;
            return await base.UpdateAsync(user, cancellationToken);
        }
    }
}
