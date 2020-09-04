using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.EntityChanges.Models;

namespace SakhaTyla.Core.Requests.EntityChanges
{
    public class GetEntityChanges : IRequest<PageModel<EntityChangeModel>>
    {
        public string EntityName { get; set; }
        public int? EntityId { get; set; }

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
