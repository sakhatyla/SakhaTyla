using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Tags
{
    public class UpdateTagValidator : AbstractValidator<UpdateTag>
    {
        public UpdateTagValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithName(x => localizer["Name"]);
        }

    }
}
