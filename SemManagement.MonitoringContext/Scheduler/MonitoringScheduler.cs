using Quartz;
using System;

namespace SemManagement.MonitoringContext.Scheduler
{
    public interface IMonitoringScheduler
    {
        void AddStationsJob<T>(string name, string group) where T : IJob;
        void AddRulesJob<T>(string name, string group) where T : IJob;
        void AddPlaylistsJob<T>(string name, string group) where T : IJob;
        void AddEchoJob<T>(string name, string group) where T : IJob;
    }

    public class MonitoringScheduler : IMonitoringScheduler
    {
        private readonly IScheduler _scheduler;

        public MonitoringScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async void AddStationsJob<T>(string name, string group) where T : IJob
        {
            var jobKey = new JobKey(name, group);
            if (await _scheduler.CheckExists(jobKey) == false)
            {
                IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

                ITrigger jobTrigger = TriggerBuilder.Create()
                    .WithIdentity(name + "Trigger", group)
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInHours(24)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(3, 0))
                    )
                    .Build();

                await _scheduler.ScheduleJob(job, jobTrigger);
            }
        }

        public async void AddRulesJob<T>(string name, string group) where T : IJob
        {
            var jobKey = new JobKey(name, group);
            if (await _scheduler.CheckExists(jobKey) == false)
            {
                IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

                ITrigger jobTrigger = TriggerBuilder.Create()
                    .WithIdentity(name + "Trigger", group)
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInHours(24)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(04, 0))
                    )
                    .Build();

                await _scheduler.ScheduleJob(job, jobTrigger);
            }
        }

        public async void AddPlaylistsJob<T>(string name, string group) where T : IJob
        {
            var jobKey = new JobKey(name, group);
            if (await _scheduler.CheckExists(jobKey) == false)
            {
                IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

                ITrigger jobTrigger = TriggerBuilder.Create()
                    .WithIdentity(name + "Trigger", group)
                    .WithDailyTimeIntervalSchedule
                    (s =>
                        s.WithIntervalInHours(24)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(03, 30))
                    )
                    .Build();

                await _scheduler.ScheduleJob(job, jobTrigger);
            }
        }

        public async void AddEchoJob<T>(string name, string group) where T : IJob
        {
            var jobKey = new JobKey(name, group);
            if (await _scheduler.CheckExists(jobKey) == false)
            {
                IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

                ITrigger jobTrigger = TriggerBuilder.Create()
                    .WithIdentity(name + "Trigger", group)
                    .StartNow()
                    .WithSimpleSchedule(t => t.WithIntervalInSeconds(30).RepeatForever())
                    .Build();

                await _scheduler.ScheduleJob(job, jobTrigger);
            }
        }
    }
}
