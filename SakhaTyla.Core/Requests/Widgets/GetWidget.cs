using System;
using MediatR;
using SakhaTyla.Core.Requests.Widgets.Models;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class GetWidget : IRequest<WidgetModel?>
    {
        public int? Id { get; set; }

        public string? Code { get; set; }
    }
}
