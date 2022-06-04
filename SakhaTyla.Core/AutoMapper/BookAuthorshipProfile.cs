using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookAuthorships;
using SakhaTyla.Core.Requests.BookAuthorships.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class BookAuthorshipProfile : Profile
    {
        public BookAuthorshipProfile()
        {
            CreateMap<BookAuthorship, BookAuthorshipModel>();
            CreateMap<BookAuthorship, BookAuthorshipShortModel>();
            CreateMap<CreateBookAuthorship, BookAuthorship>();
            CreateMap<UpdateBookAuthorship, BookAuthorship>();
        }
    }
}
