using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SakhaTyla.Core.Requests.Pages;
using SakhaTyla.Core.Requests.Routes;
using SakhaTyla.Core.Requests.Widgets;
using SakhaTyla.Core.Requests.Widgets.Models;
using SakhaTyla.Web.Front.Models;

namespace SakhaTyla.Web.Front.Pages
{
    public class AppPageModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly List<SystemWidget> _systemWidgets;

        public Core.Requests.Pages.Models.PageModel PageModel { get; set; } = null!;

        public List<Core.Requests.Pages.Models.PageModel> Parents { get; set; } = null!;

        public List<HtmlSection> Sections { get; set; } = null!;

        public List<WidgetModel> Widgets { get; set; } = null!;

        public BreadcrumbsModel BreadcrumbsModel
        {
            get
            {
                return new BreadcrumbsModel(PageModel, Parents);
            }
        }

        public ArticlesModel ArticlesModel
        {
            get
            {
                return new ArticlesModel(PageModel);
            }
        }

        public AppPageModel(IMediator mediator)
        {
            _mediator = mediator;
            _systemWidgets = new List<SystemWidget>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var path = GetPath();
            if (string.IsNullOrEmpty(path))
            {
                return NotFound();
            }
            var route = await _mediator.Send(new GetRoute() { Path = path });
            if (route == null)
            {
                return NotFound();
            }
            if (route.PageId == null)
            {
                return NotFound();
            }
            PageModel = (await _mediator.Send(new GetPage() { Id = route.PageId.Value }))!;
            Parents = await _mediator.Send(new GetPageParents() { Id = PageModel.Id });
            await LoadWidgets();
            Sections = HtmlSection.BreakToSections(PageModel.Body, Widgets, _systemWidgets);
            return Page();
        }

        private string GetPath()
        {
            var path = Request.Path.Value!;
            path = path.TrimStart('/');
            return path;
        }

        private async Task LoadWidgets()
        {
            var widgets = await _mediator.Send(new GetWidgets());
            Widgets = widgets.PageItems;
        }
    }
}
