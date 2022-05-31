using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class UpdateWidget : IRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Body { get; set; }

        public Enums.WidgetType? Type { get; set; }
    }
}
