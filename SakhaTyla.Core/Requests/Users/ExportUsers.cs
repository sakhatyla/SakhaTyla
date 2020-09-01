using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Users.Models;

namespace SakhaTyla.Core.Requests.Users
{
    public class ExportUsers : IRequest<FileContentModel>
    {
        public UserFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
