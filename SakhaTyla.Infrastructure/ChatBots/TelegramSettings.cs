using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Infrastructure.ChatBots
{
    public class TelegramSettings
    {
        public string BotToken { get; set; } = "";
        public string? ProxyUrl { get; set; }
        public string? ProxyUsername { get; set; }
        public string? ProxyPassword { get; set; }
    }
}
