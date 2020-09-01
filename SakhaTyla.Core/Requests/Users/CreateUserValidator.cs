using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Email).MaximumLength(256).EmailAddress().NotEmpty().WithName(x => localizer["Email"]);
            RuleFor(x => x.Password).Length(6, 100).NotEmpty().WithName(x => localizer["Password"]);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage(x => localizer["Passwords do not match"]);
            RuleFor(x => x.FirstName).MaximumLength(200).WithName(x => localizer["First Name"]);
            RuleFor(x => x.LastName).MaximumLength(200).WithName(x => localizer["Last Name"]);
        }

    }
}
