using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public class ChatBotMessage
    {
        public ChatBotMessage(string text, ChatBotChat chat)
        {
            Text = text;
            Chat = chat;
        }

        public string Text { get; set; }
        public ChatBotChat Chat { get; set; }
    }
}
