using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Languages.Models;

namespace SakhaTyla.Core.Requests.Languages
{
    public class GetLanguages : IRequest<PageModel<LanguageModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public LanguageFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
