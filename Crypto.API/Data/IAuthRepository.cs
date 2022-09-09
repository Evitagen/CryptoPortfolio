using System.Threading.Tasks;
using Crypto.API.Models;

namespace Crypto.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);

         Task<User> Logon(string username, string password);

         Task<bool> UserExists(string username);
    }
}