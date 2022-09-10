using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public class ChatBotInlineQuery
    {
        public ChatBotInlineQuery(string id, string query)
        {
            Id = id;
            Query = query;
        }

        public string Id { get; set; }

        public string Query { get; set; }
    }
}
