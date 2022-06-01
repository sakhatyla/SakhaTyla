using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Routes
{
    public class UpdateRouteValidator : AbstractValidator<UpdateRoute>
    {
        public UpdateRouteValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Path).MaximumLength(500).NotEmpty().Matches("^[a-z0-9/\\-\\._]+$").WithName(x => localizer["Path"]);
        }

    }
}
