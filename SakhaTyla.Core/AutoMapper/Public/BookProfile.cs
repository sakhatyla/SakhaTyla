using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Public.Books.Models;

namespace SakhaTyla.Core.AutoMapper.Public
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookModel>();
            CreateMap<BookAuthorship, BookAuthorshipModel>();
            CreateMap<BookAuthor, BookAuthorModel>();
        }
    }
}
