using LibraryManager.BusinessLogic.Models;
using LibraryManager.BusinessLogic.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManager.BusinessLogic.Interfaces
{
    public interface IBookManager
    {
        Task<BookModel> GetBookAsync(int id);
        IEnumerable<BookModel> GetBooksAsync(BookQuery query);
        Task CreateBookAsync(CreateBook createBook);
        Task UpdateBookAsync(UpdateBook updateBook);
        Task RemoveBookAsync(int id);
        Task<UpdateBook> GetBookToUpdateAsync(int id);
        Task<byte[]> GetBookImageAsync(int id);
    }
}
