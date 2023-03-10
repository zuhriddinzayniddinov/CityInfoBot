using Domen.Entities;
using Domen.Enums;
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
        if (message is null || message.Text is null)
        {
            return;
        }

        var command = message.Text.Split(' ').First().Substring(1);

        var handler = command switch
        {
            "start" or "info" => InfoProgramAsync(message),
            "delete" => DeleteLocationInfoAsync(message),
            "create" => SetInformationCityAsync(message),
            "addAdmin" => AddAdminAsync(message),
            "deleteAdmin" => DeleteAdminAsync(message),
            "getAdmins" => GetAdminsAsync(message),
            _ => GetInformationCity(message)
        };

        await handler;
    }

    private async Task GetAdminsAsync(Message message)
    {
        var role = await this.Authorization(message);

        if (role == null || role != UserRole.SuperAdmin)
        {
            return;
        }

        var users = await this.userServis.GetAdminsAsync();

        string sendText = string.Join('\n', users.Where(x => x.Role != UserRole.user)
            .Select(x => x.Name + '\t' + $"<b>{x.TelegramId}</b>"));

        await this.telegramBotClient.SendTextMessageAsync(
            text: sendText,
            chatId: message.Chat.Id,
            parseMode: ParseMode.Html);
    }

    private async Task DeleteAdminAsync(Message message)
    {
        var role = await this.Authorization(message);

        if (role != UserRole.SuperAdmin)
        { return; }

        var telegramId = long.Parse(message.Text.Split(' ')[1]);

        await this.userServis.DeleteAdminAsync(telegramId);
    }

    private async Task AddAdminAsync(Message message)
    {
        var role = await this.Authorization(message);

        if (role == null || role != UserRole.SuperAdmin)
        {
            return;
        }

        var userInfo = message.Text.Split(' ');

        var user = new Domen.Entities.User
        {
            TelegramId = long.Parse(userInfo[1]),
            Role = UserRole.Admin,
            Name = userInfo[2]
        };

        await this.userServis.SingUpAsync(user);
    }

    private async Task InfoProgramAsync(Message message)
    {
        if (message.Chat.Type != ChatType.Private)
        {
            return;
        }

        var auth = await this.Authorization(message);

        if (auth == UserRole.SuperAdmin)
        {
            string sendText = $"Xush kelibsiz <b>{auth}</b>\n" +
                "\n<b>Malumot qo'shish:</b>\n" +
                "/create dan kiyin bitta joy tashlab shahar nomini kiritasiz va pastdan ma'lumotlarini kiritasiz, ma'lumot bazaga qo'shiladi." +
                "\n<b>Misol uchun:</b>\n" +
                "/create ShaharNomi\nShaharMa'lumoti" +
                "\n<b>Malumot o'chirish:</b>\n" +
                "/delete dan kiyin bitta joy tashlab shahar nomini kiritasiz va shu shahar ma'lumotlari bazadan o'chib ketadi" +
                "\n<b>Misol uchun:</b>\n" +
                "/delete ShaharNomi" +
                "\n<b>Admin qo'shish:</b>\n" +
                "/addAdmin dan kiyin bitta joy tashlab Admin telegram Id sini kiritasiz, kiyin yana bitta joy tashlab o'ziz adminga nom berasiz va u bazaga Admin lavozimida saqlanadi" +
                "\n`<b>Misol uchun:</b>\n" +
                "/addAdmin 1234567890 AdminIsmi" +
                "\n<b>Adminlar ro'yxatini olish:</b>\n" +
                "/getAdmins bazadan barcha Admin lavozimidagilarni olib keladi" +
                "\n<b>Misol uchun:</b>\n" +
                "/getAdmins" +
                "\n<b>Admin o'chirish:</b>\n" +
                "/deleteAdmin dan kiyin bitta joy tashlab Admin telegram Id sini kiritasiz va u bazadan o'chirib tashlanadi" +
                "\n<b>Misol uchun:</b>\n" +
                "/deleteAdmin 1234567890" +
                "\n/info sizga tegishli bo'gan imkoniyatlar" +
                "\nHar qanday shahar bo'yicha ma'lumot kerakmi?\n" +
                "Unda Shahar nomini xatosiz kiriting va malumotlarni oling";

            await this.telegramBotClient.SendTextMessageAsync(
            text: sendText,
            chatId: message.Chat.Id,
            parseMode: ParseMode.Html);
        }
        else if (auth == UserRole.Admin)
        {
            string sendText = $"Xush kelibsiz <b>{auth}</b>\n" +
                "<b>Malumot qo'shish:</b>\n" +
                "/create dan kiyin bitta joy tashlab shahar nomini kiritasiz va pastdan ma'lumotlarini kiritasiz, ma'lumot bazaga qo'shiladi." +
                "\n<b>Misol uchun:</b>\n" +
                "/create ShaharNomi\nShaharMa'lumoti\n" +
                "<b>Malumot o'chirish:</b>\n" +
                "/delete dan kiyin bitta joy tashlab shahar nomini kiritasiz va shu shahar ma'lumotlari bazadan o'chib ketadi" +
                "\n<b>Misol uchun:</b>\n" +
                "/delete ShaharNomi" +
                "\n/info sizga tegishli bo'gan imkoniyatlar" +
                "\nHar qanday shahar bo'yicha ma'lumot kerakmi?\n" +
                "Unda Shahar nomini xatosiz kiriting va malumotlarni oling";

            await this.telegramBotClient.SendTextMessageAsync(
            text: sendText,
            chatId: message.Chat.Id,
            parseMode: ParseMode.Html);
        }
        else
        {
            string sendText = $"Xush kelibsiz <b>{auth}</b>\n" +
                "\n/info sizga tegishli bo'gan imkoniyatlar" +
                "Har qanday shahar bo'yicha ma'lumot kerakmi?\n" +
                "Unda Shahar nomini xatosiz kiriting va malumotlarni oling";
            await this.telegramBotClient.SendTextMessageAsync(
            text: sendText,
            chatId: message.Chat.Id,
            parseMode: ParseMode.Html);
        }
    }
    private async Task DeleteLocationInfoAsync(Message message)
    {
        if (message.Chat.Type != ChatType.Private)
        {
            return;
        }

        var auth = await this.Authorization(message);

        if (auth == null || auth == UserRole.user)
        {
            return;
        }

        var locationName = message.Text.Split(' ').Skip(1).First();

        var locationInfo = await this.locationInfoServic.GetLocationInformationAsync(locationName);

        await this.locationInfoServic.DeleteLocationInformationAsync(locationInfo);
    }
    private async Task HandleNotAvailableCommandAsync(Message message)
    {
        await this.telegramBotClient.SendTextMessageAsync
            (text: "Bunaqa buyruq yo'q, tekshirib qayta urining",
            chatId: message.Chat.Id);
    }
    private async Task GetInformationCity(Message message)
    {
        var locationInfo = await this.locationInfoServic
            .GetLocationInformationAsync(message.Text);

        var sendText = "Hech qanday ma'lumot topilmadi";

        if (locationInfo != null && locationInfo.LocationDescription != null)
        {
            sendText = locationInfo.LocationDescription;
        }

        await this.telegramBotClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: sendText);
    }
    private async Task SetInformationCityAsync(Message message)
    {
        if (message.Chat.Type != ChatType.Private)
        {
            return;
        }

        var auth = await this.Authorization(message);

        if (auth == null || auth == UserRole.user)
        {
            return;
        }

        int index = message.Text.IndexOf(' ');

        var LocationInformation = message.Text.Substring(index + 1);

        var locationInformationSplit = LocationInformation.Split('\n');
        var locationInfo = new LocationInformation
        {
            LocationName = locationInformationSplit[0],
            LocationDescription = locationInformationSplit[1]
        };

        await this.locationInfoServic
            .CreateLocationInformationAsync(locationInfo);
    }
    private async Task<UserRole> Authorization(Message message)
    {
        return await this.userServis.AuthorizationAsync(message.Chat.Id);
    }
}
