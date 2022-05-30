using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.ToTable("Routes");

            builder.HasOne(e => e.Page)
                .WithOne(e => e.Route)
                .HasForeignKey<Route>(e => e.PageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasCheckConstraint("CK_Routes_Parent", "[PageId] is not null");

            builder.HasIndex(e => e.Path)
                .IsUnique();
        }
    }
}
