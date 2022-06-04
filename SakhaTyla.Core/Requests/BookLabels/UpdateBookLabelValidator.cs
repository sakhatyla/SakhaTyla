using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class UpdateBookLabelValidator : AbstractValidator<UpdateBookLabel>
    {
        public UpdateBookLabelValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.PageId).NotEmpty().WithName(x => localizer["Page"]);
        }

    }
}
