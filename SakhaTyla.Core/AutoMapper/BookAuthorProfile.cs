using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookAuthors;
using SakhaTyla.Core.Requests.BookAuthors.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class BookAuthorProfile : Profile
    {
        public BookAuthorProfile()
        {
            CreateMap<BookAuthor, BookAuthorModel>();
            CreateMap<BookAuthor, BookAuthorShortModel>();
            CreateMap<CreateBookAuthor, BookAuthor>();
            CreateMap<UpdateBookAuthor, BookAuthor>();
        }
    }
}
