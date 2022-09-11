using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SakhaTyla.Core;
using SakhaTyla.Core.Requests.BookLabels;
using SakhaTyla.Core.Requests.BookLabels.Models;
using SakhaTyla.Core.Requests.BookPages;
using SakhaTyla.Core.Requests.Books;
using SakhaTyla.Core.Requests.Books.Models;
using SakhaTyla.Web.Front.Models;

namespace SakhaTyla.Web.Front.Pages
{
    public class BookPageModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public BookPageModel(IMediator mediator,
            IStringLocalizer<SharedResource> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        public int? PageNumber { get; set; }

        public BookModel Book { get; set; } = null!;

        public Core.Requests.BookPages.Models.BookPageModel BookPage { get; set; } = null!;

        public List<BookLabelModel> BookLabels { get; set; } = null!;

        public BreadcrumbsModel? BreadcrumbsModel
        {
            get
            {
                return new BreadcrumbsModel(Book.Name, new List<BreadcrumbModel>()
                {
                    new BreadcrumbModel(_localizer["Books"], Url.Page("/Books")!),
                });
            }
        }

        public async Task<IActionResult> OnGetAsync(string synonym, int? p)
        {
            PageNumber = p;
            var book = await _mediator.Send(new GetBook() { Synonym = synonym });
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            if (PageNumber == null)
            {
                PageNumber = Book.FirstPage;
            }
            if (PageNumber == null)
            {
                return NotFound();
            }
            var bookPage = await _mediator.Send(new GetBookPageByNumber()
            {
                BookId = Book.Id,
                Number = PageNumber.Value
            });
            if (bookPage == null)
            {
                return NotFound();
            }
            BookPage = bookPage;
            var bookLabels = await _mediator.Send(new GetBookLabels()
            {
                Filter = new BookLabelFilter()
                {
                    BookId = Book.Id,
                }
            });
            BookLabels = bookLabels.PageItems;
            return Page();
        }
    }
}
