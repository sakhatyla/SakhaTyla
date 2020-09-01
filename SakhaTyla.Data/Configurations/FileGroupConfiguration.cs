using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class FileGroupConfiguration : IEntityTypeConfiguration<FileGroup>
    {
        public void Configure(EntityTypeBuilder<FileGroup> builder)
        {
            builder.ToTable("FileGroups");
        }
    }
}
