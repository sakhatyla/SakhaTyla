using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class CreateWidget : IRequest<CreatedEntity<int>>
    {
        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? Body { get; set; }

        public Enums.WidgetType? Type { get; set; }
    }
}
