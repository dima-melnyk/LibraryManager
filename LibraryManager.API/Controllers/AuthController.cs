using LibraryManager.BusinessLogic.Interfaces;
using LibraryManager.BusinessLogic.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManager.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthManager _authManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;

        public AuthController(IAuthManager authManager, SignInManager<IdentityUser<int>> signInManager)
        {
            _authManager = authManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterUser user)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("List", "Book");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var errors = await _authManager.Register(user);

            if (!string.IsNullOrEmpty(errors))
            {
                ModelState.AddModelError("Password", errors);
                return View(user);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginUser user)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("List", "Book");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var userData = await _authManager.Login(user);

            if (userData == null)
            {
                ModelState.AddModelError("Password", "Incorrect login or password");
                return View(user);
            }
            await _signInManager.SignInAsync(userData, false);

            return RedirectToAction("List", "Book");
        }
    }
}
