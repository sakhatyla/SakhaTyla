using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SakhaTyla.Core.ChatBots;

namespace SakhaTyla.Core.TranslateChatBot
{
    public class TranslateChatBotMessageHandler : IChatBotMessageHandler
    {
        private readonly IChatBotMessageSender _chatBotMessageSender;

        public TranslateChatBotMessageHandler(IChatBotMessageSender chatBotMessageSender)
        {
            _chatBotMessageSender = chatBotMessageSender;
        }

        public async Task ProcessMessage(ChatBotMessage message, CancellationToken cancellationToken)
        {
            await _chatBotMessageSender.SendMessage(message.From.Id, "You sent: " + message.Text);
        }
    }
}
