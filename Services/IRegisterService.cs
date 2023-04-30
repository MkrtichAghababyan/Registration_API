using Registration.Models;

namespace Registration.Services
{
    public interface IRegisterService
    {
        User Register(User user);
        IEnumerable<User> Singin(string email, string password, string? promocode);
        IEnumerable<User> UpdateUser(string email, string password, User user);
        string DeleteUser(string email, string password);
    }

}
