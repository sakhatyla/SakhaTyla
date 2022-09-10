using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SakhaTyla.Core.ChatBots;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
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

        public async Task SendMessage(string chatId, string text, bool html = false, ReplyButton[]? replyButtons = null)
        {
            IReplyMarkup? replyMarkup = null;
            if (replyButtons != null)
            {
                replyMarkup = new InlineKeyboardMarkup(replyButtons.Select(b => InlineKeyboardButton.WithCallbackData(b.Text, b.Data)));
            }
            await _botClient.SendTextMessageAsync(long.Parse(chatId), text, parseMode: html ? ParseMode.Html : null, replyMarkup: replyMarkup);
        }

        public async Task EditMessage(string chatId, string messageId, string text, bool html = false, ReplyButton[]? replyButtons = null)
        {
            InlineKeyboardMarkup? replyMarkup = null;
            if (replyButtons != null)
            {
                replyMarkup = new InlineKeyboardMarkup(replyButtons.Select(b => InlineKeyboardButton.WithCallbackData(b.Text, b.Data)));
            }
            await _botClient.EditMessageTextAsync(long.Parse(chatId), int.Parse(messageId), text, parseMode: html ? ParseMode.Html : null, replyMarkup: replyMarkup);
        }

        public async Task AnswerCallbackQuery(string callbackQueryId)
        {
            await _botClient.AnswerCallbackQueryAsync(callbackQueryId);
        }
    }
}
