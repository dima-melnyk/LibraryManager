using AutoMapper;
using LibraryManager.BusinessLogic.Interfaces;
using LibraryManager.BusinessLogic.Models;
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

            if (createBook.File.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    createBook.File.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    entity.Image = fileBytes;
                }
            }

            await _context.Set<Book>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<BookModel> GetBookAsync(string name)
        {
            var book = await _context.Set<Book>().FirstOrDefaultAsync(b => b.Name == name);
            return _mapper.Map<BookModel>(book);
        }

        public IEnumerable<BookModel> GetBooksAsync(BookQuery query)
        {
            return _context.Set<Book>()
                .Where(b => b.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase) || query.Name == null)
                .Where(b => b.Subject.Contains(query.Subject, StringComparison.OrdinalIgnoreCase) || query.Subject == null)
                .Where(b => b.Grade == query.Grade || query.Grade == null)
                .Select(_mapper.Map<BookModel>);
        }

        public async Task<byte[]> GetBookImage(string name)
        {
            var book = await _context.Set<Book>().Where(b => b.Name == name)
                .Select(b => b.Image).FirstOrDefaultAsync();
            return book;
        }
    }
}
