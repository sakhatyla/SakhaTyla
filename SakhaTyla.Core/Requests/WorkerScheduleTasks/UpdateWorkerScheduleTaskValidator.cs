using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public class UpdateWorkerScheduleTaskValidator : AbstractValidator<UpdateWorkerScheduleTask>
    {
        public UpdateWorkerScheduleTaskValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Seconds).MaximumLength(50).WithName(x => localizer["Seconds"]);
            RuleFor(x => x.Minutes).MaximumLength(50).WithName(x => localizer["Minutes"]);
            RuleFor(x => x.Hours).MaximumLength(50).WithName(x => localizer["Hours"]);
            RuleFor(x => x.DayOfMonth).MaximumLength(50).WithName(x => localizer["Day of Month"]);
            RuleFor(x => x.Month).MaximumLength(50).WithName(x => localizer["Month"]);
            RuleFor(x => x.DayOfWeek).MaximumLength(50).WithName(x => localizer["Day of Week"]);
            RuleFor(x => x.Year).MaximumLength(50).WithName(x => localizer["Year"]);
        }

    }
}
