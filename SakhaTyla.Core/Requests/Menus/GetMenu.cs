using System;
using MediatR;
using SakhaTyla.Core.Requests.Menus.Models;

namespace SakhaTyla.Core.Requests.Menus
{
    public class GetMenu : IRequest<MenuModel?>
    {
        public int Id { get; set; }
    }
}
