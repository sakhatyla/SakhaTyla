using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class UpdateBookPageValidator : AbstractValidator<UpdateBookPage>
    {
        public UpdateBookPageValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.FileName).MaximumLength(200).NotEmpty().WithName(x => localizer["File Name"]);
            RuleFor(x => x.Number).NotEmpty().WithName(x => localizer["Number"]);
        }

    }
}
