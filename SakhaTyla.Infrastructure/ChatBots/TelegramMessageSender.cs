using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SakhaTyla.Core.ChatBots;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace SakhaTyla.Infrastructure.ChatBots
{
    public class TelegramMessageSender : IChatBotMessageSender
    {
        private readonly ITelegramBotClient _botClient;

        public TelegramMessageSender(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task SendMessage(string chatId, string text, bool html = false, IReplyButtons? replyButtons = null)
        {
            IReplyMarkup? replyMarkup = null;
            if (replyButtons != null)
            {
                replyMarkup = FromReplyButtons(replyButtons);
            }
            await _botClient.SendTextMessageAsync(long.Parse(chatId), text, parseMode: html ? ParseMode.Html : null, replyMarkup: replyMarkup);
        }

        public async Task EditMessage(string chatId, string messageId, string text, bool html = false, InlineReplyButtons? replyButtons = null)
        {
            InlineKeyboardMarkup? replyMarkup = null;
            if (replyButtons != null)
            {
                replyMarkup = FromInlineReplyButtons(replyButtons);
            }
            await _botClient.EditMessageTextAsync(long.Parse(chatId), int.Parse(messageId), text, parseMode: html ? ParseMode.Html : null, replyMarkup: replyMarkup);
        }

        public async Task AnswerCallbackQuery(string callbackQueryId)
        {
            await _botClient.AnswerCallbackQueryAsync(callbackQueryId);
        }

        public async Task AnswerInlineQuery(string inlineQueryId, Core.ChatBots.InlineQueryResult[] results)
        {
            var inlineQueryResults = results.Select(r => new InlineQueryResultArticle(r.Id, r.Title, new InputTextMessageContent(r.Text))
            {
                HideUrl = true,
            });
            await _botClient.AnswerInlineQueryAsync(inlineQueryId, inlineQueryResults);
        }

        private IReplyMarkup FromReplyButtons(IReplyButtons replyButtons)
        {
            if (replyButtons is InlineReplyButtons inlineReplyButtons)
            {
                return FromInlineReplyButtons(inlineReplyButtons);
            }
            else if (replyButtons is TextReplyButtons textInlineReplyButtons)
            {
                return FromTextReplyButtons(textInlineReplyButtons);
            }
            throw new NotImplementedException();
        }

        private InlineKeyboardMarkup FromInlineReplyButtons(InlineReplyButtons inlineReplyButtons)
        {
            return new InlineKeyboardMarkup(inlineReplyButtons.Buttons.Select(b => InlineKeyboardButton.WithCallbackData(b.Text, b.Data)));
        }

        private ReplyKeyboardMarkup FromTextReplyButtons(TextReplyButtons textReplyButtons)
        {
            return new ReplyKeyboardMarkup(textReplyButtons.Buttons.Select(b => new KeyboardButton(b.Text) { }))
            {
                ResizeKeyboard = true
            };
        }
    }
}
