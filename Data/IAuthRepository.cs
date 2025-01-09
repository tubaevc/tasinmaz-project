using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TasinmazProject.Entities.Concrete;

namespace TasinmazProject.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userEmail, string password);
        Task<bool> UserExists(string userEmail);
    }
}