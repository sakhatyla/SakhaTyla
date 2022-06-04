using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class CreateBookLabelValidator : AbstractValidator<CreateBookLabel>
    {
        public CreateBookLabelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.BookId).NotEmpty().WithName(x => localizer["Book"]);
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.PageId).NotEmpty().WithName(x => localizer["Page"]);
        }

    }
}
