using Domen.Entities;
using Domen.Enums;

namespace Infrastructure.Repositories.UsersRepositories;

public interface IUserRepositorie
{
    ValueTask<User> InsertUserAsync(User user);
    ValueTask<User> UpdateUserAsync(User user);
    ValueTask DeleteUserAsync(long telegramId);
    ValueTask<User> SelectUserAsync(long TelegramId);
    ValueTask<int> SaveChangesAsync();
    ValueTask<UserRole> Authorization(long telegramId);
    ValueTask<ICollection<User>> SelectUsersAsync();
}
