using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cynosura.EF;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Data.Configurations
{
    public class WorkerScheduleTaskConfiguration : IEntityTypeConfiguration<WorkerScheduleTask>
    {
        public void Configure(EntityTypeBuilder<WorkerScheduleTask> builder)
        {
            builder.ToTable("WorkerScheduleTasks");
        }
    }
}
