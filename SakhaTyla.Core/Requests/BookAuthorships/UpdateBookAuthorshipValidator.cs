using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class UpdateBookAuthorshipValidator : AbstractValidator<UpdateBookAuthorship>
    {
        public UpdateBookAuthorshipValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.AuthorId).NotEmpty().WithName(x => localizer["Author"]);
            RuleFor(x => x.Weight).NotEmpty().WithName(x => localizer["Weight"]);
        }

    }
}
