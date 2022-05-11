using System;
using MediatR;
using SakhaTyla.Core.Requests.Categories.Models;

namespace SakhaTyla.Core.Requests.Categories
{
    public class GetCategory : IRequest<CategoryModel?>
    {
        public int Id { get; set; }
    }
}
