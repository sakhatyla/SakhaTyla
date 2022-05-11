using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Files
{
    public class CreateFileValidator : AbstractValidator<CreateFile>
    {
        public CreateFileValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.ContentType).MaximumLength(200).NotEmpty().WithName(x => localizer["Content Type"]);
            RuleFor(x => x.Content).NotEmpty().WithName(x => localizer["Content"]);
            RuleFor(x => x.GroupId).NotEmpty().WithName(x => localizer["Group"]);
        }

    }
}
