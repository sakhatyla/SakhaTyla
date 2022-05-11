using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SakhaTyla.Worker.WorkerInfos
{
    public class WorkerInfoShedulerService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public WorkerInfoShedulerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var workerInfoSheduler = scope.ServiceProvider.GetService<WorkerInfoSheduler>();
                if (workerInfoSheduler != null)
                {
                    await workerInfoSheduler.ScheduleAsync();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
