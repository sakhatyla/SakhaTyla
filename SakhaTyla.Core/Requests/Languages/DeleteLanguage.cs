using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Languages
{
    public class DeleteLanguage : IRequest
    {
        public int Id { get; set; }
    }
}
