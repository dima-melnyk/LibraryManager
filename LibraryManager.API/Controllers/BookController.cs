using LibraryManager.BusinessLogic.Interfaces;
using LibraryManager.BusinessLogic.Models.Book;
using LibraryManager.BusinessLogic.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManager.API.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]CreateBook createBook)
        {
            if (!ModelState.IsValid)
            {
                return View(createBook);
            }
            await _manager.CreateBookAsync(createBook);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var updateBook = await _manager.GetBookToUpdateAsync(id);
            return View(updateBook);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]UpdateBook updateBook)
        {
            if(!ModelState.IsValid)
            {
                return View(updateBook);
            }
            await _manager.UpdateBookAsync(updateBook);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookToDelete = await _manager.GetBookAsync(id);
            return View(bookToDelete);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BookModel book)
        {
            if(book.Id == 0)
            {
                return View(book);
            }
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
