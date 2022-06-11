using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Routes;
using SakhaTyla.Migration.SourceDatabase;
using SakhaTyla.Core.Requests.Widgets;

namespace SakhaTyla.Migration.Migrations
{
    class WidgetMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;

        public WidgetMigration(SourceLoader sourceLoader, IMediator mediator)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
        }

        public async Task MigrateWidgets()
        {
            var pages = await _sourceLoader.GetPagesAsync(true);
            foreach (var page in pages)
            {
                var createWidget = new CreateWidget()
                {
                    Name = page.Title,
                    Code = page.Synonym + (page.Lang != null ? "/" + page.Lang : ""),
                    Body = page.Contents,
                    Type = Core.Enums.WidgetType.Html
                };
                await _mediator.Send(createWidget);
            }
        }
    }
}
