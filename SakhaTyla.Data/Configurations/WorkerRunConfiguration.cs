using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class WorkerRunConfiguration : IEntityTypeConfiguration<WorkerRun>
    {
        public void Configure(EntityTypeBuilder<WorkerRun> builder)
        {
            builder.ToTable("WorkerRuns");
        }
    }
}
