using System;
using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.Requests.Files
{
    public class DownloadFile : IRequest<FileContentModel>
    {
        public int Id { get; set; }
    }
}
