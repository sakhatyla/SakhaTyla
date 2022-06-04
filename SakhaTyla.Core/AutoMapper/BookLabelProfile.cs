using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookLabels;
using SakhaTyla.Core.Requests.BookLabels.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class BookLabelProfile : Profile
    {
        public BookLabelProfile()
        {
            CreateMap<BookLabel, BookLabelModel>();
            CreateMap<BookLabel, BookLabelShortModel>();
            CreateMap<CreateBookLabel, BookLabel>();
            CreateMap<UpdateBookLabel, BookLabel>();
        }
    }
}
