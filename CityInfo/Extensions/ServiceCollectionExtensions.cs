using CityInfo.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories.LocationInformationRepositories;
using Infrastructure.Repositories.UsersRepositories;
using Microsoft.EntityFrameworkCore;
using Servics.LocationInformationServic;
using Servics.UserServic;
using Telegram.Bot;

namespace CityInfo.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            string connectionString = configuration.GetValue<string>("ConnectionString");

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ILocationInformationRepositorie, LocationInformationRepositorie>();
        services.AddScoped<IUserRepositorie, UserRepositorie>();

        return services;
    }
    public static IServiceCollection AddServics(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ILocationInfoServic, LocationInfoServic>();
        services.AddScoped<IUserServic, UserServic>();

        return services;
    }
    public static IServiceCollection AddTelegramBotClient(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        string botApiKey = configuration.GetValue<string>("BotApiKey");

        services.AddSingleton<ITelegramBotClient, TelegramBotClient>(x => new TelegramBotClient(botApiKey));

        return services;
    }

    public static IServiceCollection AddUpdateHandler(
        this IServiceCollection services)
    {
        services.AddTransient<UpdateHandler>();

        return services;
    }

    public static IServiceCollection AddSwagger(
        this IServiceCollection services)
    {
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddControllerMappers(
        this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson();

        services.AddEndpointsApiExplorer();

        return services;
    }
}
