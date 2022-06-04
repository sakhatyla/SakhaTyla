using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class CreateBookAuthorValidator : AbstractValidator<CreateBookAuthor>
    {
        public CreateBookAuthorValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.LastName).MaximumLength(50).NotEmpty().WithName(x => localizer["Last Name"]);
            RuleFor(x => x.FirstName).MaximumLength(50).WithName(x => localizer["First Name"]);
            RuleFor(x => x.MiddleName).MaximumLength(50).WithName(x => localizer["Middle Name"]);
            RuleFor(x => x.NickName).MaximumLength(50).WithName(x => localizer["NickName"]);
        }

    }
}
