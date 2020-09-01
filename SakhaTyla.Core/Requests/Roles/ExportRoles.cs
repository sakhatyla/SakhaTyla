using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Roles.Models;

namespace SakhaTyla.Core.Requests.Roles
{
    public class ExportRoles : IRequest<FileContentModel>
    {
        public RoleFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
