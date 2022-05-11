using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.EntityChanges
{
    public class GetEntityChangesValidator : AbstractValidator<GetEntityChanges>
    {
        public GetEntityChangesValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.EntityName).MaximumLength(256).NotEmpty().WithName(x => localizer["EntityName"]);
        }
    }
}
