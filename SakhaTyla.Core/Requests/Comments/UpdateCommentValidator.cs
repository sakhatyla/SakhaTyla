using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Comments
{
    public class UpdateCommentValidator : AbstractValidator<UpdateComment>
    {
        public UpdateCommentValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.TextSource).NotEmpty().WithName(x => localizer["Text Source"]);
        }

    }
}
