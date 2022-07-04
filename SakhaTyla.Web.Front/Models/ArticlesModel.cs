using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Web.Front.Models
{
    public class ArticlesModel
    {
        public ArticlesModel(PageModel page)
        {
            Page = page;
        }

        public PageModel Page { get; set; }
    }
}
