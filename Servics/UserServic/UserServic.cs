using Domen.Entities;
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

    public Task<User> CreateAdminAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteAdminAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> DeleteUserAsync(User user)
    {
        var userResult = await this.userRepositorie.DeleteUserAsync(user);

        return userResult;
    }

    public Task<ICollection<User>> GetAdminsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> SingUpAsync(User user)
    {
        var userResult = await this.userRepositorie.InsertUserAsync(user);

        return userResult;
    }
}
