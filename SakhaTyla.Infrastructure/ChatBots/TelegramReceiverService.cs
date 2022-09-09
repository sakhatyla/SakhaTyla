using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SakhaTyla.Infrastructure.ChatBots.Telegram;
using Telegram.Bot;

namespace SakhaTyla.Infrastructure.ChatBots
{
    public class TelegramReceiverService : ReceiverServiceBase<TelegramUpdateHandler>
    {
        public TelegramReceiverService(
            ITelegramBotClient botClient,
            TelegramUpdateHandler updateHandler,
            ILogger<ReceiverServiceBase<TelegramUpdateHandler>> logger)
            : base(botClient, updateHandler, logger)
        {
        }
    }
}
