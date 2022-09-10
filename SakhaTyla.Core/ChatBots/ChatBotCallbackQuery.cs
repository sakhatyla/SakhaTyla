using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public class ChatBotCallbackQuery
    {
        public ChatBotCallbackQuery(string id, ChatBotMessage? message, string? data)
        {
            Id = id;
            Message = message;
            Data = data;
        }

        public string Id { get; set; }

        public ChatBotMessage? Message { get; set; }

        public string? Data { get; set; }
    }
}
