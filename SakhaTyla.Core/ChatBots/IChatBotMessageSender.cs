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
        Task EditMessage(string chatId, string messageId, string text, bool html = false, ReplyButton[]? replyButtons = null);
        Task AnswerCallbackQuery(string callbackQueryId);
        Task AnswerInlineQuery(string inlineQueryId, InlineQueryResult[] results);
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

    public class InlineQueryResult
    {
        public InlineQueryResult(string id, string title, string text)
        {
            Id = id;
            Title = title;
            Text = text;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }
}
