using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Roles
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRole>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateRoleHandler(RoleManager<Role> roleManager, IMapper mapper, IStringLocalizer<SharedResource> localizer)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateRole request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Role"], request.Id]);
            }
            _mapper.Map(request, role);
            var result = await _roleManager.UpdateAsync(role);
            result.CheckIfSucceeded();
            return Unit.Value;
        }
    }
}
