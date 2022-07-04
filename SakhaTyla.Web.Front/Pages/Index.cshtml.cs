using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SakhaTyla.Core.Requests.Pages;
using SakhaTyla.Core.Requests.Widgets;
using SakhaTyla.Core.Requests.Widgets.Models;
using SakhaTyla.Web.Front.Models;

namespace SakhaTyla.Web.Front.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly List<SystemWidget> _systemWidgets;

        public Core.Requests.Pages.Models.PageModel? PageModel { get; set; }

        public List<HtmlSection> Sections { get; set; } = null!;

        public List<WidgetModel> Widgets { get; set; } = null!;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
            _systemWidgets = new List<SystemWidget>();
        }

        public async Task OnGetAsync()
        {
            var pages = await _mediator.Send(new GetPages()
            {
                Filter = new Core.Requests.Pages.Models.PageFilter()
                {
                    Type = Core.Enums.PageType.Main
                }
            });
            PageModel = pages.PageItems.Count > 0 ? pages.PageItems[0] : null;
            await LoadWidgets();
            if (PageModel != null)
            {
                Sections = HtmlSection.BreakToSections(PageModel.Body, Widgets, _systemWidgets);
            }
        }

        private async Task LoadWidgets()
        {
            var widgets = await _mediator.Send(new GetWidgets());
            Widgets = widgets.PageItems;
        }
    }
}
