using Quartz;
using System;

namespace SemManagement.MonitoringContext.Scheduler
{
    public interface IMonitoringScheduler
    {
        void AddJob<T>(string name, string group, int interval, DateTimeOffset startAt, int stationId) where T : IJob;
        void AddJobRunOnce<T>(string name, string group) where T : IJob;
    }

    public class MonitoringScheduler : IMonitoringScheduler
    {
        private readonly IScheduler _scheduler;

        public MonitoringScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async void AddJob<T>(string name, string group, int interval, DateTimeOffset startAt, int stationId) where T : IJob
        {
            var jobKey = new JobKey(name, group);
            if (await _scheduler.CheckExists(jobKey) == false)
            {
                IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .UsingJobData("stationId", stationId)
                .Build();

                ITrigger jobTrigger = TriggerBuilder.Create()
                    .WithIdentity(name + "Trigger", group)
                    .StartAt(startAt)
                    .WithSimpleSchedule(t => t.WithIntervalInHours(interval).RepeatForever())
                    .Build();

                await _scheduler.ScheduleJob(job, jobTrigger);
            }
        }

        public async void AddJobRunOnce<T>(string name, string group) where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

            ITrigger jobTrigger = TriggerBuilder.Create()
                .WithIdentity(name + "Trigger", group)
                .StartAt(DateTimeOffset.UtcNow.AddSeconds(10))
                .Build();

            await _scheduler.ScheduleJob(job, jobTrigger);
        }
    }
}
