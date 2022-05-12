using AutoMapper;
using LibraryManager.BusinessLogic.Interfaces;
using LibraryManager.BusinessLogic.Models.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManager.BusinessLogic.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthManager(UserManager<IdentityUser<int>> userManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task Register(RegisterUser user)
        {
            var newUser = _mapper.Map<IdentityUser<int>>(user);
            newUser.PhoneNumberConfirmed = true;
            newUser.EmailConfirmed = true;

            if (await _userManager.FindByEmailAsync(newUser.Email) != null)
                throw new ArgumentException("User is already created");

            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
                throw new ArgumentException(string.Join('\n', result.Errors.Select(e => e.Description).ToArray()));

            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(identityUser, "User");
        }

        public async Task<ClaimsPrincipal> Login(LoginUser loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, loginUser.Password))) 
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, _userManager.GetRolesAsync(user).Result.FirstOrDefault()),
            };

            var id = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return new ClaimsPrincipal(id);
        }
    }
}
