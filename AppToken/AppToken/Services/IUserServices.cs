using AppToken.Models;

namespace AppToken.Services
{
    public interface IUserServices
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAllUsers();
    }
}
