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
            .AddServics(builder.Configuration)
            .AddDbContext<ApplicationDbContext>()
            .AddUpdateHandler()
            .AddTelegramBotClient(builder.Configuration)
            .AddSwagger()
            .AddControllerMappers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

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
            string baseUrl = configuration.GetValue<string>("BaseUrl");
            var webhookUrl = $"{baseUrl}/bot";
            var webhookInfo = botClient.GetWebhookInfoAsync().Result;

            if (webhookInfo is null || webhookInfo.Url != webhookUrl)
            {
                botClient.SetWebhookAsync(webhookUrl).Wait();
            }
        }
    }
}