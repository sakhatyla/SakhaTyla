using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Menus
{
    public class UpdateMenuValidator : AbstractValidator<UpdateMenu>
    {
        public UpdateMenuValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.Code).MaximumLength(200).NotEmpty().WithName(x => localizer["Code"]);
        }

    }
}
