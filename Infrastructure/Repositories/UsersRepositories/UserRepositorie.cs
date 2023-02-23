using Domen.Entities;
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

    public async ValueTask<User> DeleteUserAsync(User user)
    {
        var user2 = this.applicationDbContext
            .Set<User>()
            .Remove(user);

        await this.SaveChangesAsync();

        return user;
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

    public async ValueTask<User> UpdateUserAsync(User user)
    {
        var user2 = this.applicationDbContext
            .Set<User>()
            .Update(user);

        await this.SaveChangesAsync();

        return user;
    }
}
