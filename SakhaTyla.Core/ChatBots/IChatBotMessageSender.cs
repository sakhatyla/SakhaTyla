using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public interface IChatBotMessageSender
    {
        Task SendMessage(string chatId, string text, bool html = false, ReplyButton[]? replyButtons = null);
    }

    public class ReplyButton
    {
        public ReplyButton(string text, string data)
        {
            Text = text;
            Data = data;
        }

        public string Text { get; set; }
        public string Data { get; set; }
    }
}
