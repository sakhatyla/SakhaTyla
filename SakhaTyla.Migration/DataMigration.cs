using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SakhaTyla.Migration.Migrations;

namespace SakhaTyla.Migration
{
    internal class DataMigration
    {
        private readonly ILogger<DataMigration> _logger;
        private readonly PageMigration _pageMigration;

        public DataMigration(ILogger<DataMigration> logger,
            PageMigration pageMigration)
        {
            _logger = logger;
            _pageMigration = pageMigration;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await _pageMigration.MigratePages();

            _logger.LogInformation($"Migration completed");
        }
    }
}
