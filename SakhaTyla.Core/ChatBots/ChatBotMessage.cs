using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public class ChatBotMessage
    {
        public ChatBotMessage(string messageId, ChatBotChat chat, string? text)
        {
            MessageId = messageId;
            Chat = chat;
            Text = text;
        }

        public string MessageId { get; set; }
        public ChatBotChat Chat { get; set; }
        public string? Text { get; set; }
    }
}
