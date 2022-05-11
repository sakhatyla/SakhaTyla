using System;
using MediatR;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.Requests.Files
{
    public class GetFile : IRequest<FileModel?>
    {
        public int Id { get; set; }
    }
}
