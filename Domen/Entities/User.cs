using Domen.Enums;

namespace Domen.Entities;

public class User
{
    public long TelegramId { get; set; }
    public UserRole Role { get; set; } = UserRole.user;
    public string Name { get; set; }
}
