using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class UpdateMenuItemWeight : IRequest
    {
        public int MenuId { get; set; }

        public int? ParentId { get; set; }

        public int[]? Ids { get; set; }
    }
}
