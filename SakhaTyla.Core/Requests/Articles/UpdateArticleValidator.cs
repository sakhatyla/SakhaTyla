using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Articles
{
    public class UpdateArticleValidator : AbstractValidator<UpdateArticle>
    {
        public UpdateArticleValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Title).MaximumLength(200).NotEmpty().WithName(x => localizer["Title"]);
            RuleFor(x => x.TextSource).NotEmpty().WithName(x => localizer["Text Source"]);
            RuleFor(x => x.FromLanguageId).NotEmpty().WithName(x => localizer["From Language"]);
            RuleFor(x => x.ToLanguageId).NotEmpty().WithName(x => localizer["To Language"]);
            RuleFor(x => x.Fuzzy).NotEmpty().WithName(x => localizer["Fuzzy"]);
            RuleFor(x => x.CategoryId);
        }

    }
}
