using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookPages;
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class BookPageProfile : Profile
    {
        public BookPageProfile()
        {
            CreateMap<BookPage, BookPageModel>();
            CreateMap<BookPage, BookPageShortModel>();
            CreateMap<CreateBookPage, BookPage>();
            CreateMap<UpdateBookPage, BookPage>();
            CreateMap<BookPage, Requests.Public.BookPages.Models.BookPageModel>();
        }
    }
}
