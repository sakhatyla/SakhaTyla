using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SakhaTyla.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
        public string? ReturnUrl { get; set; }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
    }
}
