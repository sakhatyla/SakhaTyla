using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Profile
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfile>
    {
        public UpdateProfileValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.FirstName).MaximumLength(200).WithName(x => localizer["First Name"]);
            RuleFor(x => x.LastName).MaximumLength(200).WithName(x => localizer["Last Name"]);
        }
    }
}
