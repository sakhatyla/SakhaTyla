using System;
using MediatR;
using SakhaTyla.Core.Requests.MenuItems.Models;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class GetMenuItem : IRequest<MenuItemModel?>
    {
        public int Id { get; set; }
    }
}
