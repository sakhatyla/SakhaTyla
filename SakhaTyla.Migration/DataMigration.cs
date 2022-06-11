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
        private readonly CategoryMigration _categoryMigration;

        public DataMigration(ILogger<DataMigration> logger,
            PageMigration pageMigration,
            WidgetMigration widgetMigration,
            CategoryMigration categoryMigration)
        {
            _logger = logger;
            _pageMigration = pageMigration;
            _widgetMigration = widgetMigration;
            _categoryMigration = categoryMigration;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            //await _pageMigration.MigratePages();
            //await _widgetMigration.MigrateWidgets();
            await _categoryMigration.MigrateCategories();

            _logger.LogInformation($"Migration completed");
        }
    }
}
