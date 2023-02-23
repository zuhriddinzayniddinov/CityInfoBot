using Domen.Entities;

namespace Infrastructure.Repositories.UsersRepositories;

public interface IUserRepositorie
{
    ValueTask<User> InsertUserAsync(User user);
    ValueTask<User> UpdateUserAsync(User user);
    ValueTask<User> DeleteUserAsync(User user);
    ValueTask<User> SelectUserAsync(long TelegramId);
    ValueTask<int> SaveChangesAsync();
}
