using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class CreateBookPageValidator : AbstractValidator<CreateBookPage>
    {
        public CreateBookPageValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.BookId).NotEmpty().WithName(x => localizer["Book"]);
            RuleFor(x => x.FileName).MaximumLength(200).NotEmpty().WithName(x => localizer["File Name"]);
            RuleFor(x => x.Number).NotEmpty().WithName(x => localizer["Number"]);
        }

    }
}
