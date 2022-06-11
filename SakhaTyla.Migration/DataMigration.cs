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
        private readonly WidgetMigration _widgetMigration;

        public DataMigration(ILogger<DataMigration> logger,
            PageMigration pageMigration,
            WidgetMigration widgetMigration)
        {
            _logger = logger;
            _pageMigration = pageMigration;
            _widgetMigration = widgetMigration;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            //await _pageMigration.MigratePages();
            await _widgetMigration.MigrateWidgets();

            _logger.LogInformation($"Migration completed");
        }
    }
}
