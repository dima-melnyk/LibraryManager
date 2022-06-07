using LibraryManager.BusinessLogic.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using System.Threading.Tasks;

namespace LibraryManager.BusinessLogic.Interfaces
{
    public interface IAuthManager
    {
        Task<string> Register(RegisterUser user);
        Task<IdentityUser<int>> Login(LoginUser loginUser);
    }
}
