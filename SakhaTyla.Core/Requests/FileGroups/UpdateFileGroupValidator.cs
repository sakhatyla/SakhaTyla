using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class UpdateFileGroupValidator : AbstractValidator<UpdateFileGroup>
    {
        public UpdateFileGroupValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.Type).NotEmpty().WithName(x => localizer["Type"]);
            RuleFor(x => x.Location).MaximumLength(200).WithName(x => localizer["Location"]);
            RuleFor(x => x.Accept).MaximumLength(1000).WithName(x => localizer["Accept"]);
        }

    }
}
