using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cynosura.Core.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Messaging.WorkerInfos;
using SakhaTyla.Core.Messaging.WorkerRuns;
using SakhaTyla.Worker.Infrastructure;
using SakhaTyla.Worker.Jobs;

namespace SakhaTyla.Worker.WorkerInfos
{
    public class WorkerInfoSheduler
    {
        private readonly IEntityRepository<WorkerInfo> _workerInfoRepository;
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly ISchedulerFactory _schedulerFactory;

        public WorkerInfoSheduler(IEntityRepository<WorkerInfo> workerInfoRepository,
            IEntityRepository<WorkerRun> workerRunRepository,
            ISchedulerFactory schedulerFactory)
        {
            _workerInfoRepository = workerInfoRepository;
            _workerRunRepository = workerRunRepository;
            _schedulerFactory = schedulerFactory;
        }

        public async Task ScheduleAsync(int? workerInfoId = null)
        {
            var workerInfos = await _workerInfoRepository.GetEntities()
                .Include(e => e.ScheduleTasks)
                .Where(e => workerInfoId == null || e.Id == workerInfoId)
                .ToListAsync();

            var scheduler = await _schedulerFactory.GetScheduler();

            foreach (var workerInfo in workerInfos)
            {
                var jobKey = new JobKey(RunWorkerInfoJob.JobKey);
                var triggers = await scheduler.GetTriggersOfJob(jobKey);
                foreach (var trigger in triggers)
                {
                    var message = trigger.JobDataMap[QuartzData.Message] as RunWorkerInfo;
                    if (message != null && message.Id == workerInfo.Id)
                    {
                        await scheduler.UnscheduleJob(trigger.Key);
                    }
                }
                foreach (var scheduleTask in workerInfo.ScheduleTasks)
                {
                    var jobData = new JobDataMap
                    {
                        [QuartzData.Message] = new RunWorkerInfo
                        {
                            Id = workerInfo.Id,
                        }
                    };
                    var cronExpression = $"{(!string.IsNullOrWhiteSpace(scheduleTask.Seconds) ? scheduleTask.Seconds : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.Minutes) ? scheduleTask.Minutes : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.Hours) ? scheduleTask.Hours : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.DayOfMonth) ? scheduleTask.DayOfMonth : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.Month) ? scheduleTask.Month : "*")} " +
                        $"{(!string.IsNullOrWhiteSpace(scheduleTask.DayOfWeek) ? scheduleTask.DayOfWeek : "?")}";
                    if (!string.IsNullOrEmpty(scheduleTask.Year))
                    {
                        cronExpression += $" {scheduleTask.Year}";
                    }
                    var trigger = TriggerBuilder.Create()
                        .WithCronSchedule(cronExpression)
                        .ForJob(jobKey)
                        .UsingJobData(jobData)
                        .Build();

                    await scheduler.ScheduleJob(trigger);
                }
            }
        }

        public async Task RunAsync(StartWorkerRun startWorkerRun)
        {
            var workerRun = await _workerRunRepository.GetEntities()
                .Where(e => e.Id == startWorkerRun.WorkerRunId)
                .FirstAsync();
            var scheduler = await _schedulerFactory.GetScheduler();

            var jobKey = new JobKey($"{StartWorkerRunJob.JobKey}_{workerRun.WorkerInfoId}");
            await EnsureJobExistsAsync(jobKey, scheduler);

            var jobData = new JobDataMap
            {
                [QuartzData.Message] = startWorkerRun
            };
            await scheduler.TriggerJob(jobKey, jobData);
        }

        private static async Task EnsureJobExistsAsync(JobKey jobKey, IScheduler scheduler)
        {
            var job = await scheduler.GetJobDetail(jobKey);
            if (job == null)
            {
                job = JobBuilder.Create<StartWorkerRunJob>()
                    .WithIdentity(jobKey)
                    .StoreDurably()
                    .Build();
                await scheduler.AddJob(job, false);
            }
        }
    }
}
