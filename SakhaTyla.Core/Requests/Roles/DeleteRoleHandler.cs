using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Roles
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRole>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteRoleHandler(RoleManager<Role> roleManager, IStringLocalizer<SharedResource> localizer)
        {
            _roleManager = roleManager;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteRole request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Role"], request.Id]);
            }
            var result = await _roleManager.DeleteAsync(role);
            result.CheckIfSucceeded();
            return Unit.Value;
        }
    }
}
