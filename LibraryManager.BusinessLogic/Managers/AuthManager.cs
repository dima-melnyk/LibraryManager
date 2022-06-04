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
        private readonly IMapper _mapper;

        public AuthManager(UserManager<IdentityUser<int>> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<string> Register(RegisterUser user)
        {
            var newUser = _mapper.Map<IdentityUser<int>>(user);
            newUser.PhoneNumberConfirmed = true;
            newUser.EmailConfirmed = true;

            if (await _userManager.FindByEmailAsync(newUser.Email) != null)
                return "User is already created";

            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
                return string.Join('\n', result.Errors.Select(e => e.Description).ToArray());

            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(identityUser, "User");

            return null;
        }

        public async Task<IdentityUser<int>> Login(LoginUser loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, loginUser.Password))) 
            {
                return null;
            }

            return user;
        }
    }
}
