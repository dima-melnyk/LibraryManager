using AutoMapper;
using LibraryManager.BusinessLogic.Interfaces;
using LibraryManager.BusinessLogic.Models.Book;
using LibraryManager.BusinessLogic.Queries;
using LibraryManager.DataAccess;
using LibraryManager.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.BusinessLogic.Managers
{
    public class BookManager : IBookManager
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public BookManager(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateBookAsync(CreateBook createBook)
        {
            var entity = _mapper.Map<Book>(createBook);

            if (createBook.File != null)
            {
                if (createBook.File.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        createBook.File.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        entity.Image = fileBytes;
                    }
                }
            }
            
            await _context.Set<Book>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<BookModel> GetBookAsync(int id)
        {
            var book = await GetBookById(id);
            return _mapper.Map<BookModel>(book);
        }

        public IEnumerable<BookModel> GetBooksAsync(BookQuery query)
        {
            return _context.Set<Book>()
                .Where(b => b.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase) || query.Name == null)
                .Where(b => b.Subject.Contains(query.Subject) || query.Subject == null)
                .Where(b => b.Grade == query.Grade || query.Grade == null)
                .Select(_mapper.Map<BookModel>);
        }

        public async Task UpdateBookAsync(UpdateBook updateBook)
        {
            var bookToUpdate = await GetBookById(updateBook.Id);
            _mapper.Map(updateBook, bookToUpdate);
            _context.Update(bookToUpdate);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookAsync(int id)
        {
            var bookToDelete = await GetBookById(id);
            _context.Remove(bookToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<UpdateBook> GetBookToUpdateAsync(int id)
        {
            var bookToUpdate = await GetBookById(id);
            var updateBook = new UpdateBook();
            return _mapper.Map(bookToUpdate, updateBook);
        }

        public async Task<byte[]> GetBookImageAsync(int id)
        {
            var book = await _context.Set<Book>().Where(b => b.Id == id)
                .Select(b => b.Image).FirstOrDefaultAsync();
            return book;
        }

        public Task<List<string>> GetAllSubjects() => _context.Set<Book>().Select(b => b.Subject).Distinct().ToListAsync();
        public Task<List<int>> GetAllGrades() => _context.Set<Book>().Select(b => b.Grade).Distinct().ToListAsync();

        private Task<Book> GetBookById(int id) => _context.Set<Book>().FirstOrDefaultAsync(b => b.Id == id);
    }
}
