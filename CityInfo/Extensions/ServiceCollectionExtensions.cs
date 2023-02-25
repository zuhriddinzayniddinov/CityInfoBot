using CityInfo.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories.LocationInformationRepositories;
using Infrastructure.Repositories.UsersRepositories;
using Microsoft.EntityFrameworkCore;
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
            string connectionString = @"Server=(localdb)\ProjectModels;Database=LocationInformation;Trusted_Connection=True;MultipleActiveResultSets=true;";

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<ILocationInformationRepositorie, LocationInformationRepositorie>();
        services.AddScoped<IUserRepositorie, UserRepositorie>();


        return services;
    }

    public static IServiceCollection AddTelegramBotClient(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        string botApiKey = "5571209805:AAEreOQoNKAcN-EcuxfGSBVkxhw9qKUtw1Y";

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
