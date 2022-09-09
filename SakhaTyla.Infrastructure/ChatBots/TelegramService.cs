using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SakhaTyla.Infrastructure.ChatBots.Telegram;
using Telegram.Bot;

namespace SakhaTyla.Infrastructure.ChatBots
{
    public class TelegramService : PollingServiceBase<TelegramReceiverService>
    {
        public TelegramService(IServiceProvider serviceProvider, ILogger<TelegramService> logger)
            : base(serviceProvider, logger)
        {
        }
    }
}
