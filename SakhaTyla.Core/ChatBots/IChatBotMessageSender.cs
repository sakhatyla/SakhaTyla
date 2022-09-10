using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public interface IChatBotMessageSender
    {
        Task SendMessage(string chatId, string text, bool html = false, IReplyButtons? replyButtons = null);
        Task EditMessage(string chatId, string messageId, string text, bool html = false, InlineReplyButtons? replyButtons = null);
        Task AnswerCallbackQuery(string callbackQueryId);
        Task AnswerInlineQuery(string inlineQueryId, InlineQueryResult[] results);
    }

    public interface IReplyButtons
    {

    }

    public class TextReplyButtons : IReplyButtons
    {
        public TextReplyButtons(IEnumerable<TextReplyButton> buttons)
        {
            Buttons = buttons.ToList();
        }

        public List<TextReplyButton> Buttons { get; set; }
    }

    public class TextReplyButton
    {
        public TextReplyButton(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }

    public class InlineReplyButtons : IReplyButtons
    {
        public InlineReplyButtons(IEnumerable<InlineReplyButton> buttons)
        {
            Buttons = buttons.ToList();
        }

        public List<InlineReplyButton> Buttons { get; set; }
    }

    public class InlineReplyButton
    {
        public InlineReplyButton(string text, string data)
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
