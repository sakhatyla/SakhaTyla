using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Web.Front.Models
{
    public class BreadcrumbsModel
    {
        public BreadcrumbsModel(string name)
        {
            Name = name;
            Parents = new List<BreadcrumbModel>();
        }

        public BreadcrumbsModel(string name, List<BreadcrumbModel> parents)
        {
            Name = name;
            Parents = parents;
        }

        public BreadcrumbsModel(PageModel pageModel, List<PageModel> parents)
        {
            Name = pageModel.GetShortName();
            Parents = parents
                .Select(p => new BreadcrumbModel(p.GetShortName(), $"/{p.Route!.Path}"))
                .ToList();
        }

        public List<BreadcrumbModel> Parents { get; set; }

        public string Name { get; set; }
    }

    public class BreadcrumbModel
    {
        public BreadcrumbModel(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
