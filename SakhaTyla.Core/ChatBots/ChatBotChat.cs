using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public class ChatBotChat
    {
        public ChatBotChat(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public bool Private { get; set; }
    }
}
