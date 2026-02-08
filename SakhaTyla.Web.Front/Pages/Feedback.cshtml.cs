using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace SakhaTyla.Web.Front.Pages
{
    public class FeedbackModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public FeedbackModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            var googleFormUrl = _configuration["Feedback:GoogleFormUrl"];
            if (string.IsNullOrEmpty(googleFormUrl))
            {
                return NotFound();
            }
            return Redirect(googleFormUrl);
        }
    }
}
