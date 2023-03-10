using Domen.Entities;
using Domen.Enums;
using Infrastructure.Repositories.LocationInformationRepositories;
using Infrastructure.Repositories.UsersRepositories;

namespace Servics.UserServic;

public class UserServic : IUserServic
{
    private readonly IUserRepositorie userRepositorie;

    public UserServic(
        IUserRepositorie userRepositorie)
    {
        this.userRepositorie = userRepositorie;
    }

    public async Task<UserRole> AuthorizationAsync(long telegramId)
    {
        return await this.userRepositorie.Authorization(telegramId);
    }

    public Task<User> CreateAdminAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAdminAsync(long telegramId)
    {
        await this.userRepositorie.DeleteUserAsync(telegramId);
    }

    public async Task DeleteUserAsync(long telegramId)
    {
        await this.userRepositorie.DeleteUserAsync(telegramId);
    }

    public async Task<ICollection<User>> GetAdminsAsync()
    {
        var users = await this.userRepositorie.SelectUsersAsync();

        return users;
    }

    public async Task<User> SingUpAsync(User user)
    {
        var userResult = await this.userRepositorie.InsertUserAsync(user);

        return userResult;
    }
}
