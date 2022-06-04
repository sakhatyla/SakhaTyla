using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class CreateBookAuthorshipValidator : AbstractValidator<CreateBookAuthorship>
    {
        public CreateBookAuthorshipValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.BookId).NotEmpty().WithName(x => localizer["Book"]);
            RuleFor(x => x.AuthorId).NotEmpty().WithName(x => localizer["Author"]);
            RuleFor(x => x.Weight).NotEmpty().WithName(x => localizer["Weight"]);
        }

    }
}
