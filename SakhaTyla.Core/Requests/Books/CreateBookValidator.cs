using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Books
{
    public class CreateBookValidator : AbstractValidator<CreateBook>
    {
        public CreateBookValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.Synonym).MaximumLength(100).NotEmpty().Matches("^[a-z0-9\\-_]+$").WithName(x => localizer["Synonym"]);
            RuleFor(x => x.Hidden).NotEmpty().WithName(x => localizer["Hidden"]);
            RuleFor(x => x.Cover).MaximumLength(100).WithName(x => localizer["Cover"]);
        }

    }
}
