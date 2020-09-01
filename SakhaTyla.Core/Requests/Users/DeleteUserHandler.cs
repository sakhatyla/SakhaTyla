using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteUserHandler(UserManager<User> userManager, IStringLocalizer<SharedResource> localizer)
        {
            _userManager = userManager;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["User"], request.Id]);
            }
            var result = await _userManager.DeleteAsync(user);
            result.CheckIfSucceeded();
            return Unit.Value;
        }
    }
}
