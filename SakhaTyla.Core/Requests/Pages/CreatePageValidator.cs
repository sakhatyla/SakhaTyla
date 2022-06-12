using FluentValidation;
using Microsoft.Extensions.Localization;
using SakhaTyla.Core.Requests.Routes;

namespace SakhaTyla.Core.Requests.Pages
{
    public class CreatePageValidator : AbstractValidator<CreatePage>
    {
        public CreatePageValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Type).IsInEnum().NotEmpty().WithName(x => localizer["Type"]);
            RuleFor(x => x.Name).MaximumLength(200).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.ShortName).MaximumLength(100).WithName(x => localizer["Short Name"]);
            RuleFor(x => x.ParentId);
            RuleFor(x => x.Header).MaximumLength(500).WithName(x => localizer["Header"]);
            RuleFor(x => x.Body);
            RuleFor(x => x.MetaTitle).MaximumLength(500).WithName(x => localizer["Meta Title"]);
            RuleFor(x => x.MetaKeywords).MaximumLength(2000).WithName(x => localizer["Meta Keywords"]);
            RuleFor(x => x.MetaDescription);
            RuleFor(x => x.ImageId);
            RuleFor(x => x.Preview);
            RuleFor(x => x.Route).NotEmpty().SetValidator(new UpdateRouteValidator(localizer)!).WithName(x => localizer["Route"]);
            RuleFor(x => x.PublicationDate);
        }

    }
}
