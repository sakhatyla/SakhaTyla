using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SakhaTyla.Migration.Data
{
    public class IdEntityConfiguration : IEntityTypeConfiguration<IdEntity>
    {
        public void Configure(EntityTypeBuilder<IdEntity> builder)
        {
            builder.HasNoKey();
        }
    }
}
