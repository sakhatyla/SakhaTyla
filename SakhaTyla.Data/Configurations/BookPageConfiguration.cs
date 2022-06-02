using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class BookPageConfiguration : IEntityTypeConfiguration<BookPage>
    {
        public void Configure(EntityTypeBuilder<BookPage> builder)
        {
            builder.ToTable("BookPages");
        }
    }
}
