using Domen.Entities;
using Infrastructure.Repositories.LocationInformationRepositories;
using Infrastructure.Repositories.UsersRepositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using Servics.LocationInformationServic;
using Servics.UserServic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CityInfo.Services;

public class UpdateHandler
{
    private readonly ITelegramBotClient telegramBotClient;
    private readonly ILocationInfoServic locationInfoServic;
    private readonly IUserServic userServis;

    public UpdateHandler(
        ITelegramBotClient telegramBotClient,
        ILocationInfoServic locationInfoServic,
        IUserServic userServis)
    {
        this.telegramBotClient = telegramBotClient;
        this.locationInfoServic = locationInfoServic;
        this.userServis = userServis;
    }

    internal async Task UpdateHandlerAsync(Update update)
    {
        var handler = update.Type switch
        {
            UpdateType.Message => HandleCommandAsync(update.Message),
            _ => HandleNotAvailableCommandAsync(update.Message)
        };

        await handler;
    }

    private async Task HandleCommandAsync(Message message)
    {
        var handler = message.Text switch
        {
            "/SuperAdmin" => AdminPanelAsync(message),
            "/delete" => DeleteLocationInfoAsync(message),
            _ => GetInformationCity(message)
        };

        await handler;
    }

    private async Task AdminPanelAsync(Message message)
    {
        await this.telegramBotClient.SendTextMessageAsync(
            text: "Xush kelibsiz Admin",
            chatId: message.Chat.Id);
    }
    private async Task DeleteLocationInfoAsync(Message message)
    {
        var locationInfo = await this.locationInfoServic.GetLocationInformationAsync(message.Text);
        await this.locationInfoServic.DeleteLocationInformationAsync(locationInfo);
    }
    private async Task HandleNotAvailableCommandAsync(Message message)
    {
        await this.telegramBotClient.SendTextMessageAsync
            (text: "Bunaqa buyruq yo'q, tekshirib qayta urining",
            chatId: message.Chat.Id);
    }

    private async Task GetInformationCity(Message messenge)
    {
        if(messenge.Chat.Type != ChatType.Private)
        {
            return;
        }

        var locationInfo = await this.locationInfoServic
            .GetLocationInformationAsync(messenge.Text);

        var sendText = "Hech qanday ma'lumot topilmadi";

        if (locationInfo != null && locationInfo.LocationDescription != null)
        {
            sendText = locationInfo.LocationDescription;
        }

        await this.telegramBotClient.SendTextMessageAsync(
            chatId: messenge.Chat.Id,
            text: sendText);
    }

    private async Task SetInformationCityAsync(Message messenge)
    {
        if (messenge.Chat.Type != ChatType.Private)
        {
            return;
        }

        var locationInfo = new LocationInformation
        {
            LocationName = messenge.Text + " mavzu1",
            LocationDescription = messenge.Text + " matin1"
        };

        await this.locationInfoServic
            .CreateLocationInformationAsync(locationInfo);
    }
}
