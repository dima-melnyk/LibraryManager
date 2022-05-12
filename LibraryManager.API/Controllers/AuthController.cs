using LibraryManager.BusinessLogic.Interfaces;
using LibraryManager.BusinessLogic.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManager.API.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await _authManager.Register(user);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginUser user)
        {
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
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userData, new AuthenticationProperties() { IsPersistent = false });

            return RedirectToAction("List", "Book");
        }
    }
}
