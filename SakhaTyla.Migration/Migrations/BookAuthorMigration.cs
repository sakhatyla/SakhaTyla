using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Routes;
using SakhaTyla.Migration.SourceDatabase;
using SakhaTyla.Core.Requests.BookAuthors;

namespace SakhaTyla.Migration.Migrations
{
    class BookAuthorMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;

        public BookAuthorMigration(SourceLoader sourceLoader, IMediator mediator)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
        }

        public async Task MigrateBookAuthors()
        {
            var bookAuthors = await _sourceLoader.GetBookAuthorsAsync();
            foreach (var bookAuthor in bookAuthors)
            {
                var createBookAuthor = new CreateBookAuthor()
                {
                    LastName = bookAuthor.LastName,
                    FirstName = bookAuthor.FirstName,
                    MiddleName = bookAuthor.MiddleName,
                    NickName = bookAuthor.NickName,
                };
                await _mediator.Send(createBookAuthor);
            }
        }
    }
}
