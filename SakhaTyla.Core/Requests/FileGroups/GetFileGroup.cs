using System;
using MediatR;
using SakhaTyla.Core.Requests.FileGroups.Models;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class GetFileGroup : IRequest<FileGroupModel>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
