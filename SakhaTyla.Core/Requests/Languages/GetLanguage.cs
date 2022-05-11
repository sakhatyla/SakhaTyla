using System;
using MediatR;
using SakhaTyla.Core.Requests.Languages.Models;

namespace SakhaTyla.Core.Requests.Languages
{
    public class GetLanguage : IRequest<LanguageModel?>
    {
        public int Id { get; set; }
    }
}
