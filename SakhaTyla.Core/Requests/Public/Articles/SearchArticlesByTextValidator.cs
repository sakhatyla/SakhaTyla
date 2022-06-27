using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Public.Articles
{
    public class SearchArticlesByTextValidator : AbstractValidator<SearchArticlesByText>
    {
        public SearchArticlesByTextValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Query).NotEmpty().WithName(x => localizer["Query"]);
        }
    }
}
