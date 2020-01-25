using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SemManagement.API.Scheduler
{
    public class MonitoringScheduler
    {
        private readonly IScheduler _scheduler;

        public MonitoringScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async void AddJob<T>(string name, string group, int interval) where T : Quartz.IJob
        {
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

            ITrigger jobTrigger = TriggerBuilder.Create()
                .WithIdentity(name + "Trigger", group)
                .StartNow()
                .WithSimpleSchedule(t => t.WithIntervalInSeconds(interval).RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(job, jobTrigger);
        }
    }
}
