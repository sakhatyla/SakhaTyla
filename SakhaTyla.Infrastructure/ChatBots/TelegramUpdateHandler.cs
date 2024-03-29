﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SakhaTyla.Core.ChatBots;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SakhaTyla.Infrastructure.ChatBots
{
    public class TelegramUpdateHandler : IUpdateHandler
    {
        private readonly ILogger<TelegramUpdateHandler> _logger;
        private readonly IChatBotMessageHandler _chatBotMessageHandler;

        public TelegramUpdateHandler(ILogger<TelegramUpdateHandler> logger,
            IChatBotMessageHandler chatBotMessageHandler)
        {
            _logger = logger;
            _chatBotMessageHandler = chatBotMessageHandler;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
        {
            var handler = update switch
            {
                // UpdateType.Unknown:
                // UpdateType.ChannelPost:
                // UpdateType.EditedChannelPost:
                // UpdateType.ShippingQuery:
                // UpdateType.PreCheckoutQuery:
                // UpdateType.Poll:
                { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
                { EditedMessage: { } message } => BotOnMessageReceived(message, cancellationToken),
                { CallbackQuery: { } callbackQuery } => BotOnCallbackQueryReceived(callbackQuery, cancellationToken),
                { InlineQuery: { } inlineQuery } => BotOnInlineQueryReceived(inlineQuery, cancellationToken),
                { ChosenInlineResult: { } chosenInlineResult } => BotOnChosenInlineResultReceived(chosenInlineResult, cancellationToken),
                _ => UnknownUpdateHandlerAsync(update, cancellationToken)
            };

            await handler;
        }

        private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received message type: {MessageType}", message.Type);
            if (message.Text is not { } messageText)
                return;

            await _chatBotMessageHandler.ProcessMessage(ToChatBotMessage(message), cancellationToken);
        }

        // Process Inline Keyboard callback data
        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

            await _chatBotMessageHandler.ProcessCallbackQuery(new ChatBotCallbackQuery(callbackQuery.Id, callbackQuery.Message != null ? ToChatBotMessage(callbackQuery.Message) : null, callbackQuery.Data), cancellationToken);
        }

        #region Inline Mode

        private async Task BotOnInlineQueryReceived(InlineQuery inlineQuery, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline query from: {InlineQueryFromId}", inlineQuery.From.Id);

            await _chatBotMessageHandler.ProcessInlineQuery(new ChatBotInlineQuery(inlineQuery.Id, inlineQuery.Query), cancellationToken);
        }

        private Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline result: {ChosenInlineResultId}", chosenInlineResult.ResultId);
            return Task.CompletedTask;
        }

        #endregion

        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
            return Task.CompletedTask;
        }

        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogInformation("HandleError: {ErrorMessage}", errorMessage);

            // Cooldown in case of network connection error
            if (exception is RequestException)
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }

        private ChatBotChat ToChatBotChat(Chat chat)
        {
            return new ChatBotChat($"{chat.Id}")
            {
                Private = chat.Type == ChatType.Private,
            };
        }

        private ChatBotMessage ToChatBotMessage(Message message)
        {
            return new ChatBotMessage($"{message.MessageId}", ToChatBotChat(message.Chat), message.Text);
        }
    }
}
