using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SakhaTyla.Web.Front.Infrastructure
{
    public class PascalCaseJsonAttribute : ActionFilterAttribute
    {
        private static readonly SystemTextJsonOutputFormatter Formatter = new SystemTextJsonOutputFormatter(new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
        });

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
                objectResult.Formatters.Add(Formatter);
        }
    }
}
