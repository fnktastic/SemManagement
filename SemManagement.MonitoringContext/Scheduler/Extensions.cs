using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SemManagement.MonitoringContext.Scheduler.Jobs;
using SemManagement.MonitoringContext.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SemManagement.MonitoringContext.Scheduler
{
    public static class Extensions
    {
        public static Collection<T> ToCollection<T>(this IEnumerable<T> enumerable)
        {
            var collection = new Collection<T>();
            foreach (T i in enumerable)
                collection.Add(i);
            return collection;
        }

        public static async void AddQuartz(this IServiceCollection services)
        {
            services.AddTransient<StartMonitoringJob>();

            services.AddTransient<SetUpRuleJob>();

            services.AddTransient<StartMonitoringPlaylistJob>();

            services.AddTransient<EchoJob>();

            var container = services.BuildServiceProvider();

            var jobFactory = new JobFactory(container);

            var schedulerFactory = new StdSchedulerFactory();

            var scheduler = await schedulerFactory.GetScheduler();

            scheduler.JobFactory = jobFactory;

            services.AddSingleton<IScheduler>(scheduler);

            var monitoringScheduler = new MonitoringScheduler(scheduler);

            services.AddSingleton<IMonitoringScheduler, MonitoringScheduler>();

            await scheduler.Start();
        }
    }
}
