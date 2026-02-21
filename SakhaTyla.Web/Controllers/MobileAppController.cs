using System.Collections.Generic;
using Cynosura.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Route("api")]
    public class MobileAppController : Controller
    {
        private readonly Dictionary<string, string> _settings;

        public MobileAppController(IOptionsSnapshot<Dictionary<string, string>> options)
        {
            _settings = options.Get("MobileApp");
        }

        [HttpPost("GetMobileConfig")]
        public IActionResult GetMobileConfig([FromBody] GetMobileConfigRequest request)
        {
            if (!_settings.TryGetValue(request.Key, out var value))
                return NotFound();
            return Ok(value);
        }
    }

    public class GetMobileConfigRequest
    {
        public string Key { get; set; } = string.Empty;
    }
}
