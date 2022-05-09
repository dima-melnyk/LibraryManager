using LibraryManager.BusinessLogic.Interfaces;
using LibraryManager.BusinessLogic.Models;
using LibraryManager.BusinessLogic.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryManager.API.Controllers
{
    public class BookController : Controller
    {
        private IBookManager _manager;
        
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

        public async Task<IActionResult> GetPhoto([FromQuery]string name)
        {
            byte[] cover = await _manager.GetBookImage(name);
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
