using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cynosura.Core.Messaging;
using Microsoft.Extensions.Hosting;

namespace SakhaTyla.Infrastructure.Messaging
{
    public class MessagingWorker : IHostedService
    {
        private readonly IMessagingService _messagingService;

        public MessagingWorker(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _messagingService.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _messagingService.StopAsync(cancellationToken);
        }
    }
}
