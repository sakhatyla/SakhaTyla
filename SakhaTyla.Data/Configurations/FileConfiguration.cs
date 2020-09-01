using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("Files");

            builder
                 .HasOne(e => e.Group)
                 .WithMany()
                 .HasForeignKey(e => e.GroupId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
