using LibraryManager.BusinessLogic.Interfaces;
using LibraryManager.BusinessLogic.Models;
using LibraryManager.BusinessLogic.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManager.API.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookManager _manager;
        
        public BookController(IBookManager manager)
        {
            _manager = manager;
        }

        public IActionResult List([FromQuery]BookQuery query)
        {
            var books = _manager.GetBooksAsync(query);
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]CreateBook createBook)
        {
            await _manager.CreateBookAsync(createBook);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Update(int id)
        {
            var updateBook = await _manager.GetBookToUpdateAsync(id);
            return View(updateBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]UpdateBook updateBook)
        {
            await _manager.UpdateBookAsync(updateBook);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bookToDelete = await _manager.GetBookAsync(id);
            return View(bookToDelete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BookModel book)
        {
            await _manager.RemoveBookAsync(book.Id);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> GetPhoto(int id)
        {
            byte[] cover = await _manager.GetBookImageAsync(id);
            if (cover != null)
            {
                return File(cover, "image/png");
            }
            else
            {
                return null;
            }
        }
    }
}
