using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Routes;
using SakhaTyla.Migration.SourceDatabase;
using SakhaTyla.Core.Requests.Books;

namespace SakhaTyla.Migration.Migrations
{
    class BookMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;

        public BookMigration(SourceLoader sourceLoader, IMediator mediator)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
        }

        public async Task MigrateBooks()
        {
            var books = await _sourceLoader.GetBooksAsync();
            foreach (var book in books)
            {
                var createBook = new CreateBook()
                {
                    Name = book.Name,
                    Synonym = book.Synonym,
                    Cover = book.Cover,
                    Hidden = book.IsHidden,
                };
                await _mediator.Send(createBook);
            }
        }
    }
}
