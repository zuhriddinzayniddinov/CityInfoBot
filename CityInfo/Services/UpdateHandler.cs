using Domen.Entities;
using Infrastructure.Repositories.LocationInformationRepositories;
using Infrastructure.Repositories.UsersRepositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CityInfo.Services;

public class UpdateHandler
{
    private readonly ILocationInformationRepositorie locationInformationRepositorie;
    private readonly IUserRepositorie userRepositorie;
    private readonly ITelegramBotClient telegramBotClient;

    public UpdateHandler(
        ILocationInformationRepositorie locationInformationRepositorie,
        IUserRepositorie userRepositorie,
        ITelegramBotClient telegramBotClient)
    {
        this.locationInformationRepositorie = locationInformationRepositorie;
        this.userRepositorie = userRepositorie;
        this.telegramBotClient = telegramBotClient;
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
            "/delete" => 
            _ => GetInformationCity(message)
        };

        await handler;
    }

    private async Task AdminPanelAsync(Message message)
    {
        await SetInformationCityAsync(message);
    }

    private Task HandleNotAvailableCommandAsync(Message message)
    {
        throw new NotImplementedException();
    }

    public async Task GetInformationCity(Message messenge)
    {
        if(messenge.Chat.Type != ChatType.Private)
        {
            return;
        }

        var locationInfo = await this.locationInformationRepositorie
            .SelectLocationInformationAsync(messenge.Text);

        var sendText = "Hech qanday ma'lumot topilmadi";

        if (locationInfo != null && locationInfo.LocationDescription != null)
        {
            sendText = locationInfo.LocationDescription;
        }

        await this.telegramBotClient.SendTextMessageAsync(
            chatId: messenge.Chat.Id,
            text: sendText);
    }

    public async Task SetInformationCityAsync(Message messenge)
    {
        if (messenge.Chat.Type != ChatType.Private)
        {
            return;
        }

        var locationInfo = new LocationInformation
        {
            LocationName = messenge.Text + " mavzu",
            LocationDescription = messenge.Text + " matin"
        };

        await this.locationInformationRepositorie
            .InsertLocationInformationAsync(locationInfo);
    }
}
