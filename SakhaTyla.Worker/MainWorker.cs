using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using SakhaTyla.Worker.Infrastructure;

namespace SakhaTyla.Worker
{
    class MainWorker : IHostedService
    {
        private readonly IScheduler _scheduler;

        public MainWorker(IServiceProvider serviceProvider, CoreLogProvider coreLogProvider)
        {
            LogProvider.SetCurrentLogProvider(coreLogProvider);
            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = new ServiceProviderJobFactory(serviceProvider);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _scheduler.Shutdown(true, cancellationToken);
        }
    }
}
