using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace SakhaTyla.Worker.Infrastructure
{
    public class ServiceProviderJobFactory : SimpleJobFactory
    {
        private readonly Dictionary<IJob, IServiceScope> _scopes = new Dictionary<IJob, IServiceScope>();
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                var scope = _serviceProvider.CreateScope();
                var job = (IJob)scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType);
                _scopes[job] = scope;
                return job;
            }
            catch (Exception e)
            {
                throw new SchedulerException(
                    $"Problem while instantiating job '{bundle.JobDetail.Key}' from the ServiceProviderJobFactory.", e);
            }
        }

        public override void ReturnJob(IJob job)
        {
            var scope = _scopes[job];
            _scopes.Remove(job);
            base.ReturnJob(job);
            scope.Dispose();
        }
    }
}
