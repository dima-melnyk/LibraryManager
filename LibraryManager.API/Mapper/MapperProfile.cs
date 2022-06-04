using AutoMapper;
using LibraryManager.BusinessLogic.Models.Auth;
using LibraryManager.BusinessLogic.Models.Book;
using LibraryManager.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryManager.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Book, BookModel>();
            CreateMap<CreateBook, Book>()
                .ForMember(b => b.Image, opt => opt.Ignore());
            CreateMap<UpdateBook, Book>()
                .ForMember(b => b.Image, opt => opt.Ignore());
            CreateMap<Book, UpdateBook>();

            CreateMap<RegisterUser, IdentityUser<int>>();
        }
    }
}
