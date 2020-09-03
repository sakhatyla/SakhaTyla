using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

            builder.HasIndex(e => e.Title);

            builder
                .HasOne(e => e.FromLanguage)
                .WithMany()
                .HasForeignKey(e => e.FromLanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.ToLanguage)
                .WithMany()
                .HasForeignKey(e => e.ToLanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
