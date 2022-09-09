using Cynosura.Core.Services.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SakhaTyla.Core;
using SakhaTyla.Core.Requests.Books;
using SakhaTyla.Core.Requests.Books.Models;
using SakhaTyla.Web.Front.Models;

namespace SakhaTyla.Web.Front.Pages
{
    public class BooksModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public BooksModel(IMediator mediator,
            IStringLocalizer<SharedResource> localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        public PageModel<BookModel> Books { get; set; } = null!;

        public BreadcrumbsModel BreadcrumbsModel
        {
            get
            {
                return new BreadcrumbsModel(_localizer["Books"]);
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Books = await _mediator.Send(new GetBooks()
            {
                Filter = new BookFilter()
                {
                    Hidden = false,
                }
            });
            return Page();
        }
    }
}
