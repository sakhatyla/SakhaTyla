using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Routes;
using SakhaTyla.Migration.SourceDatabase;
using SakhaTyla.Core.Requests.Books;
using SakhaTyla.Core.Requests.BookAuthors;
using SakhaTyla.Core.Requests.BookAuthorships;
using SakhaTyla.Core.Requests.BookPages;
using SakhaTyla.Core.Requests.BookLabels;

namespace SakhaTyla.Migration.Migrations
{
    class BookMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;

        private readonly Dictionary<int, int> _bookIdMap = new();
        private readonly Dictionary<int, int> _bookAuthorIdMap = new();
        private readonly Dictionary<int, int> _bookPageIdMap = new();

        public BookMigration(SourceLoader sourceLoader, IMediator mediator)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
        }

        public async Task MigrateBookData()
        {
            await MigrateBooks();

            await MigrateBookAuthors();

            await MigrateBookAuthorships();

            await MigrateBookPages();

            await MigrateBookLabels();
        }

        private async Task MigrateBooks()
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
                var createdBook = await _mediator.Send(createBook);
                _bookIdMap[book.Id] = createdBook.Id;
            }
        }

        private async Task MigrateBookAuthors()
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
                var createdBookAuthor = await _mediator.Send(createBookAuthor);
                _bookAuthorIdMap[bookAuthor.Id] = createdBookAuthor.Id;
            }
        }

        private async Task MigrateBookAuthorships()
        {
            var bookAuthorships = await _sourceLoader.GetBookAuthorshipsAsync();
            foreach (var bookAuthorship in bookAuthorships)
            {
                var createBookAuthorship = new CreateBookAuthorship()
                {
                    BookId = _bookIdMap[bookAuthorship.BookId],
                    AuthorId = _bookAuthorIdMap[bookAuthorship.AuthorId],
                    Weight = 10 - bookAuthorship.Order,
                };
                await _mediator.Send(createBookAuthorship);
            }
        }

        private async Task MigrateBookPages()
        {
            var bookPages = await _sourceLoader.GetBookPagesAsync();
            foreach (var bookPage in bookPages)
            {
                var createBookPage = new CreateBookPage()
                {
                    BookId = _bookIdMap[bookPage.BookId],
                    FileName = bookPage.FileName,
                    Number = bookPage.Number,
                };
                var createdBookPage = await _mediator.Send(createBookPage);
                _bookPageIdMap[bookPage.Id] = createdBookPage.Id;
            }
        }

        private async Task MigrateBookLabels()
        {
            var bookLabels = await _sourceLoader.GetBookLabelsAsync();
            foreach (var bookLabel in bookLabels)
            {
                var createBookLabel = new CreateBookLabel()
                {
                    BookId = _bookIdMap[bookLabel.BookId],
                    Name = bookLabel.Name,
                    PageId = _bookPageIdMap[bookLabel.PageId],
                };
                await _mediator.Send(createBookLabel);
            }
        }
    }
}
