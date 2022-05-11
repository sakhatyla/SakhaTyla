using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Roles
{
    public class CreateRoleHandler : IRequestHandler<CreateRole, CreatedEntity<int>>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public CreateRoleHandler(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateRole request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<CreateRole, Role>(request);
            var result = await _roleManager.CreateAsync(role);
            result.CheckIfSucceeded();
            return new CreatedEntity<int>(role.Id);
        }
    }
}
