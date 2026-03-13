using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Public.SpellCheck.Models;
using SakhaTyla.Core.SpellCheck;

namespace SakhaTyla.Core.Requests.Public.SpellCheck
{
    public class FixSpellingHandler : IRequestHandler<FixSpelling, FixSpellingModel>
    {
        private readonly ISpellCheckService _spellCheckService;

        public FixSpellingHandler(ISpellCheckService spellCheckService)
        {
            _spellCheckService = spellCheckService;
        }

        public Task<FixSpellingModel> Handle(FixSpelling request, CancellationToken cancellationToken)
        {
            var fixedText = _spellCheckService.FixSpelling(request.Language ?? "sah", request.Text ?? "");
            return Task.FromResult(new FixSpellingModel(fixedText));
        }
    }
}
