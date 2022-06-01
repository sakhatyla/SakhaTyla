using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class UpdateMenuItemValidator : AbstractValidator<UpdateMenuItem>
    {
        public UpdateMenuItemValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.Url).MaximumLength(200).WithName(x => localizer["Url"]);
            RuleFor(x => x.ParentId);
        }

    }
}
