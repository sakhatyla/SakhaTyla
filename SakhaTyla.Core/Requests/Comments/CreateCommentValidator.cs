using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.Comments
{
    public class CreateCommentValidator : AbstractValidator<CreateComment>
    {
        public CreateCommentValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.ContainerId).NotEmpty().WithName(x => localizer["Container"]);
            RuleFor(x => x.TextSource).NotEmpty().WithName(x => localizer["Text Source"]);
            RuleFor(x => x.AuthorId);
            RuleFor(x => x.ParentId);
        }

    }
}
