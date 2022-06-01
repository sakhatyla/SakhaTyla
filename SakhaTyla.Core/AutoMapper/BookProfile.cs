using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Books;
using SakhaTyla.Core.Requests.Books.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookModel>();
            CreateMap<Book, BookShortModel>();
            CreateMap<CreateBook, Book>();
            CreateMap<UpdateBook, Book>();
        }
    }
}
