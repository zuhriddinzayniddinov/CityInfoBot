using Domen.Entities;
using Domen.Enums;

namespace Servics.UserServic;

public interface IUserServic
{
    Task<User> CreateAdminAsync(User user);
    Task DeleteAdminAsync(long telegramId);
    Task<ICollection<User>> GetAdminsAsync();
    Task<User> SingUpAsync(User user);
    Task DeleteUserAsync(long telegramId);
    Task<UserRole> AuthorizationAsync(long telegramId);
}
