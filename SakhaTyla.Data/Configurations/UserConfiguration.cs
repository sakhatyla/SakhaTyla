using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<IdentityUserRole<int>>(
                    userRole => userRole.HasOne<Role>()
                        .WithMany()
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired(),
                    userRole => userRole.HasOne<User>()
                        .WithMany()
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired());
        }
    }
}
