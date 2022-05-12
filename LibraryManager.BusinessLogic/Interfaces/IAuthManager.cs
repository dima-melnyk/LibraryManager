using LibraryManager.BusinessLogic.Models.Auth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManager.BusinessLogic.Interfaces
{
    public interface IAuthManager
    {
        Task Register(RegisterUser user);
        Task<ClaimsPrincipal> Login(LoginUser loginUser);
    }
}
