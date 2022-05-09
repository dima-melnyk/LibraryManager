using AutoMapper;
using LibraryManager.BusinessLogic.Models;
using LibraryManager.DataAccess.Entities;

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
        }
    }
}
