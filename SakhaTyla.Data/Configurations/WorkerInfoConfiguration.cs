using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class WorkerInfoConfiguration : IEntityTypeConfiguration<WorkerInfo>
    {
        public void Configure(EntityTypeBuilder<WorkerInfo> builder)
        {
            builder.ToTable("WorkerInfos");
        }
    }
}
