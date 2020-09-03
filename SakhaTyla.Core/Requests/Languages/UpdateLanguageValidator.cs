using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Languages
{
    public class UpdateLanguageValidator : AbstractValidator<UpdateLanguage>
    {
        public UpdateLanguageValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(50).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.Code).MaximumLength(10).NotEmpty().WithName(x => localizer["Code"]);
        }

    }
}
