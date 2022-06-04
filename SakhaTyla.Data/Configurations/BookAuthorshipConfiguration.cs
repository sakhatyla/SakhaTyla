using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class BookAuthorshipConfiguration : IEntityTypeConfiguration<BookAuthorship>
    {
        public void Configure(EntityTypeBuilder<BookAuthorship> builder)
        {
            builder.ToTable("BookAuthorships");
        }
    }
}
