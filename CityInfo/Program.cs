using CityInfo.Extensions;
using CityInfo.Middlewares;
using Infrastructure.Contexts;
using Telegram.Bot;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddInfrastructure(builder.Configuration)
            .AddDbContext<ApplicationDbContext>() // DbContext ni qo'shing
            .AddUpdateHandler()
            .AddTelegramBotClient(builder.Configuration)
            .AddSwagger()
            .AddControllerMappers();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        app.MapControllers();

        SetWebHook(app, builder.Configuration);

        app.Run();
    }
    private static void SetWebHook(
           IApplicationBuilder builder,
           IConfiguration configuration)
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            var baseUrl = "https://304b-178-218-201-17.in.ngrok.io";
            var webhookUrl = $"{baseUrl}/bot";
            var webhookInfo = botClient.GetWebhookInfoAsync().Result;

            if (webhookInfo is null || webhookInfo.Url != webhookUrl)
            {
                botClient.SetWebhookAsync(webhookUrl).Wait();
            }
        }
    }
}