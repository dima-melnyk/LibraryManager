using LibraryManager.BusinessLogic.Models;
using LibraryManager.BusinessLogic.Queries;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LibraryManager.BusinessLogic.Interfaces
{
    public interface IBookManager
    {
        Task<BookModel> GetBookAsync(string name);
        IEnumerable<BookModel> GetBooksAsync(BookQuery query);
        Task CreateBookAsync(CreateBook createBook);
        Task<byte[]> GetBookImage(string name);
    }
}
