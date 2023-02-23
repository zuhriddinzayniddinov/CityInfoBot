using Domen.Entities;
using Domen.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.TelegramId);

        builder.Property(x => x.Name)
            .HasMaxLength(25)
            .IsRequired();

        builder.Property(x => x.Role)
            .IsRequired();

        builder.HasData(GenerateUsers());
    }
    private List<User> GenerateUsers()
    {
        return new List<User>
        {
            new User
            {
                TelegramId = 1,
                Name = "xxxx",
                Role = UserRole.SuperAdmin
            },
            new User
            {
                TelegramId= 501130550,
                Name = "ZUHRIDDIN",
                Role = UserRole.Admin
            }
        };
    }
}
