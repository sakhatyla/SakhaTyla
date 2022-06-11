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
        private readonly BookMigration _bookMigration;
        private readonly CommentMigration _commentMigration;

        public DataMigration(ILogger<DataMigration> logger,
            PageMigration pageMigration,
            WidgetMigration widgetMigration,
            CategoryMigration categoryMigration,
            BookMigration bookMigration,
            CommentMigration commentMigration)
        {
            _logger = logger;
            _pageMigration = pageMigration;
            _widgetMigration = widgetMigration;
            _categoryMigration = categoryMigration;
            _bookMigration = bookMigration;
            _commentMigration = commentMigration;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            //await _pageMigration.MigratePageData();
            //await _widgetMigration.MigrateWidgets();
            //await _categoryMigration.MigrateCategories();
            //await _bookMigration.MigrateBookData();
            await _commentMigration.MigrateComments();

            _logger.LogInformation($"Migration completed");
        }
    }
}
