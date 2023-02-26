using Domen.Entities;

namespace Servics.UserServic;

public interface IUserServic
{
    Task<User> CreateAdminAsync(User user);
    Task<User> DeleteAdminAsync(User user);
    Task<ICollection<User>> GetAdminsAsync();
    Task<User> SingUpAsync(User user);
    Task<User> DeleteUserAsync(User user);
}
