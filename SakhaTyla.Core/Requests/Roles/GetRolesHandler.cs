using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Roles.Models;

namespace SakhaTyla.Core.Requests.Roles
{
    public class GetRolesHandler : IRequestHandler<GetRoles, PageModel<RoleModel>>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public GetRolesHandler(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<PageModel<RoleModel>> Handle(GetRoles request, CancellationToken cancellationToken)
        {
            IQueryable<Role> query = _roleManager.Roles;
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var roles = await query.ToPagedListAsync(request.PageIndex, request.PageSize);
            return roles.Map<Role, RoleModel>(_mapper);
        }

    }
}
