using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Categories
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategory>
    {
        public CreateCategoryValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithName(x => localizer["Name"]);
        }

    }
}
