using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class CreateWidgetValidator : AbstractValidator<CreateWidget>
    {
        public CreateWidgetValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.Code).MaximumLength(200).NotEmpty().WithName(x => localizer["Code"]);
            RuleFor(x => x.Body);
            RuleFor(x => x.Type).IsInEnum().NotEmpty().WithName(x => localizer["Type"]);
        }

    }
}
