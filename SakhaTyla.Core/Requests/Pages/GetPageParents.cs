using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.Pages
{
    public class GetPageParents : IRequest<List<PageModel>>
    {
        public int Id { get; set; }
    }
}
