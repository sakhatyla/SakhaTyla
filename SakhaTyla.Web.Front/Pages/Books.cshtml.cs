using Cynosura.Core.Services.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SakhaTyla.Core.Requests.Books;
using SakhaTyla.Core.Requests.Books.Models;
using SakhaTyla.Web.Front.Models;

namespace SakhaTyla.Web.Front.Pages
{
    public class BooksModel : PageModel
    {
        private readonly IMediator _mediator;

        public BooksModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public PageModel<BookModel> Books { get; set; } = null!;

        public BreadcrumbsModel BreadcrumbsModel
        {
            get
            {
                return new BreadcrumbsModel("Books");
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
