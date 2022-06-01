using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class CreateMenuItemValidator : AbstractValidator<CreateMenuItem>
    {
        public CreateMenuItemValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.MenuId).NotEmpty().WithName(x => localizer["Menu"]);
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.Url).MaximumLength(200).WithName(x => localizer["Url"]);
            RuleFor(x => x.ParentId);
        }

    }
}
