using System.Threading.Tasks;
using Universal.EBI.MVC.Models;

namespace Universal.EBI.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);
        Task<UserResponseLogin> Register(UserRegister userRegister);
        Task RealizarLogin(UserResponseLogin response);
        Task Logout();
        bool TokenExpirado();
        Task<bool> RefreshTokenValido();
    }
}
