using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SakhaTyla.Infrastructure.ChatBots.Telegram
{
    public abstract class PollingServiceBase<TReceiverService> : BackgroundService
        where TReceiverService : IReceiverService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public PollingServiceBase(
            IServiceProvider serviceProvider,
            ILogger<PollingServiceBase<TReceiverService>> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting polling service");

            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            // Make sure we receive updates until Cancellation Requested,
            // no matter what errors our ReceiveAsync get
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Create new IServiceScope on each iteration.
                    // This way we can leverage benefits of Scoped TReceiverService
                    // and typed HttpClient - we'll grab "fresh" instance each time
                    using var scope = _serviceProvider.CreateScope();
                    var receiver = scope.ServiceProvider.GetRequiredService<TReceiverService>();

                    await receiver.ReceiveAsync(stoppingToken);
                }
                // Update Handler only captures exception inside update polling loop
                // We'll catch all other exceptions here
                // see: https://github.com/TelegramBots/Telegram.Bot/issues/1106
                catch (Exception ex)
                {
                    _logger.LogError("Polling failed with exception: {Exception}", ex);

                    // Cooldown if something goes wrong
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }
}
