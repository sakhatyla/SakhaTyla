using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class BookLabelConfiguration : IEntityTypeConfiguration<BookLabel>
    {
        public void Configure(EntityTypeBuilder<BookLabel> builder)
        {
            builder.ToTable("BookLabels");

            builder.HasOne(e => e.Page)
                .WithMany()
                .HasForeignKey(e => e.PageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
