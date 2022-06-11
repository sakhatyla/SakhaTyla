using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Routes;
using SakhaTyla.Core.Requests.Pages;
using SakhaTyla.Migration.SourceDatabase;

namespace SakhaTyla.Migration.Migrations
{
    class PageMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;

        public PageMigration(SourceLoader sourceLoader, IMediator mediator)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
        }

        public async Task MigratePages()
        {
            var pages = await _sourceLoader.GetPagesAsync(false);
            foreach (var page in pages)
            {
                var createPage = new CreatePage()
                {
                    Type = Core.Enums.PageType.General,
                    Name = page.Title,
                    Route = new UpdateRoute()
                    {
                        Path = "pages/" + page.Synonym + (page.Lang != null ? "/" + page.Lang : ""),
                    },
                    Body = page.Contents,
                };
                await _mediator.Send(createPage);
            }
        }
    }
}
