using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Requests.Profile;
using SakhaTyla.Core.Requests.Profile.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize]
    [ValidateModel]
    [Route("api")]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetProfile")]
        public async Task<ProfileModel> GetProfileAsync(GetProfile getProfile)
        {
            return await _mediator.Send(getProfile);
        }

        [HttpPost("UpdateProfile")]
        public async Task<Unit> UpdateProfileAsync([FromBody] UpdateProfile updateProfile)
        {
            return await _mediator.Send(updateProfile);
        }
    }
}
