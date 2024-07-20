using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SakhaTyla.Core.Requests.Public.Articles;
using SakhaTyla.Core.Requests.Public.Articles.Models;
using SakhaTyla.Core.Requests.Widgets;
using SakhaTyla.Core.Requests.Widgets.Models;
using SakhaTyla.Web.Front.Models;

namespace SakhaTyla.Web.Front.Pages
{
    public class TranslateModel : PageModel
    {
        private readonly IMediator _mediator;

        public TranslateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public TranslateFormModel TranslateForm { get; set; } = new TranslateFormModel();

        [BindProperty(Name = "q", SupportsGet = true)]
        public string? Query { get; set; }

        public Core.Requests.Public.Articles.Models.TranslateModel? Translation { get; set; }

        public WidgetModel? TranslateAfterWidget { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            TranslateForm.Query = Query;

            if (!string.IsNullOrEmpty(Query))
            {
                Translation = await _mediator.Send(new Translate()
                {
                    Query = Query,
                });
            }

            TranslateAfterWidget = await _mediator.Send(new GetWidget() { Code = "translate-after" });

            return Page();
        }

        public ArticlePartialModel GetArticlePartialModel(ArticleModel article, bool showLanguage)
        {
            return new ArticlePartialModel(article)
            {
                ShowLanguage = showLanguage
            };
        }
    }
}
