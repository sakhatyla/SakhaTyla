using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class PageConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Pages");

            builder.HasOne(a => a.CommentContainer)
                .WithOne(b => b.Page)
                .HasForeignKey<Page>(b => b.CommentContainerId);
        }
    }
}
