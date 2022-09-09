using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public class ChatBotMessage
    {
        public ChatBotMessage(string text, ChatBotUser from)
        {
            Text = text;
            From = from;
        }

        public string Text { get; set; }
        public ChatBotUser From { get; set; }
    }
}
