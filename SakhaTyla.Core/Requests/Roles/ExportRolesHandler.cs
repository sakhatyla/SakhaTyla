using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Formatters;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Roles.Models;

namespace SakhaTyla.Core.Requests.Roles
{
    public class ExportRolesHandler : IRequestHandler<ExportRoles, FileContentModel>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportRolesHandler(RoleManager<Role> roleManager, IExcelFormatter excelFormatter, IMapper mapper)
        {
            _roleManager = roleManager;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportRoles request, CancellationToken cancellationToken)
        {
            IQueryable<Role> query = _roleManager.Roles;
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var roles = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<Role>, List<RoleModel>>(roles);
            return await _excelFormatter.GetExcelFileAsync(models, "Roles");
        }

    }
}
