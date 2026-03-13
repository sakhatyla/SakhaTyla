using MediatR;
using SakhaTyla.Core.Requests.Public.SpellCheck.Models;

namespace SakhaTyla.Core.Requests.Public.SpellCheck
{
    public class FixSpelling : IRequest<FixSpellingModel>
    {
        public string? Language { get; set; }
        public string? Text { get; set; }
    }
}
