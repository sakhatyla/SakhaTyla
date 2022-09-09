using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public interface IChatBotMessageSender
    {
        Task SendMessage(string id, string text);
    }
}
