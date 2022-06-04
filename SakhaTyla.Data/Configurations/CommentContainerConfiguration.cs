using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class CommentContainerConfiguration : IEntityTypeConfiguration<CommentContainer>
    {
        public void Configure(EntityTypeBuilder<CommentContainer> builder)
        {
            builder.ToTable("CommentContainers");
        }
    }
}
