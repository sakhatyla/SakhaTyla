using System;
using MediatR;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.Pages
{
    public class GetPage : IRequest<PageModel?>
    {
        public int Id { get; set; }
    }
}
