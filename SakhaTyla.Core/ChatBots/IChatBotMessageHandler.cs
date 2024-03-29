﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SakhaTyla.Core.ChatBots
{
    public interface IChatBotMessageHandler
    {
        Task ProcessMessage(ChatBotMessage message, CancellationToken cancellationToken);

        Task ProcessCallbackQuery(ChatBotCallbackQuery callbackQuery, CancellationToken cancellationToken);

        Task ProcessInlineQuery(ChatBotInlineQuery inlineQuery, CancellationToken cancellationToken);
    }
}
