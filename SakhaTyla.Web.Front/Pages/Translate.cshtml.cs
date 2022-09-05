﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SakhaTyla.Core.Requests.Public.Articles;
using SakhaTyla.Core.Requests.Public.Articles.Models;
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

            return Page();
        }
    }
}
