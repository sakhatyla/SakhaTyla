using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Public.BookLabels.Models;

namespace SakhaTyla.Core.AutoMapper.Public
{
    public class BookLabelProfile : Profile
    {
        public BookLabelProfile()
        {
            CreateMap<BookLabel, BookLabelModel>();
        }
    }
}
