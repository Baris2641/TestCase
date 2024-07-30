using Alpata.Models;
using System.Threading.Tasks;

namespace Alpata.Services
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterModel model);
        Task<User> Login(LoginModel model);
        Task<bool> UserExists(string email);
    }
}
