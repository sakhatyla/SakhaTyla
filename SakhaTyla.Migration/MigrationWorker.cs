using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SakhaTyla.Migration
{
    internal class MigrationWorker : IHostedService
    {
        private readonly DataMigration _dataMigration;
        private readonly IServiceProvider _serviceProvider;

        public MigrationWorker(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            _dataMigration = scope.ServiceProvider.GetRequiredService<DataMigration>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _dataMigration.RunAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
