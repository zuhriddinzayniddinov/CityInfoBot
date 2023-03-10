using Domen.Entities;
using Domen.Enums;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.UsersRepositories;

public class UserRepositorie : IUserRepositorie
{
    private readonly ApplicationDbContext applicationDbContext;

    public UserRepositorie(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async ValueTask<UserRole> Authorization(long telegramId)
    {
        var userRole = this.applicationDbContext.Set<User>()
            .Where(x => x.TelegramId == telegramId)
            .Select(x => x.Role)
            .FirstOrDefault();

        return userRole;
    }

    public async ValueTask DeleteUserAsync(long telegramId)
    {
        this.applicationDbContext
            .Set<User>()
            .Remove(new User { TelegramId = telegramId});

        await this.SaveChangesAsync();
    }

    public async ValueTask<User> InsertUserAsync(User user)
    {
        var user2 = await this.applicationDbContext
            .Set<User>()
            .AddAsync(user);

        await this.SaveChangesAsync();

        return user;
    }

    public async ValueTask<int> SaveChangesAsync()
    {
        return await this.applicationDbContext.SaveChangesAsync();
    }

    public async ValueTask<User> SelectUserAsync(long TelegramId)
    {
        var user = await this.applicationDbContext
            .Set<User>()
            .Where(x => x.TelegramId == TelegramId)
            .FirstAsync();

        await this.SaveChangesAsync();

        return user;
    }

    public async ValueTask<ICollection<User>> SelectUsersAsync()
    {
        var users = await this.applicationDbContext.Set<User>()
            .Select(x => x).ToListAsync();

        return users;
    }

    public async ValueTask<User> UpdateUserAsync(User user)
    {
        var user2 = this.applicationDbContext
            .Set<User>()
            .Update(user);

        await this.SaveChangesAsync();

        return user;
    }
}
