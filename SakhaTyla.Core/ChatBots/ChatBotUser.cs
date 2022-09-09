using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public class ChatBotUser
    {
        public ChatBotUser(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
