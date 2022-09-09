using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SakhaTyla.Core.ChatBots;
using Telegram.Bot;

namespace SakhaTyla.Infrastructure.ChatBots
{
    public class TelegramMessageSender : IChatBotMessageSender
    {
        private readonly ITelegramBotClient _botClient;

        public TelegramMessageSender(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task SendMessage(string id, string text)
        {
            await _botClient.SendTextMessageAsync(long.Parse(id), text);
        }
    }
}
