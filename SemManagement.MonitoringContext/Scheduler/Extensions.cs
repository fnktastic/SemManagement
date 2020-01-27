using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SemManagement.MonitoringContext.Scheduler.Jobs;
using SemManagement.MonitoringContext.Services;

namespace SemManagement.MonitoringContext.Scheduler
{
    public static class Extensions
    {
        public static async void AddQuartz(this IServiceCollection services)
        {
            services.AddTransient<HelloJob>();

            services.AddTransient<StartMonitoringJob>();

            var container = services.BuildServiceProvider();

            var jobFactory = new JobFactory(container);

            var schedulerFactory = new StdSchedulerFactory();

            var scheduler = await schedulerFactory.GetScheduler();

            scheduler.JobFactory = jobFactory;

            services.AddSingleton<IScheduler>(scheduler);

            var monitoringScheduler = new MonitoringScheduler(scheduler);

            services.AddSingleton<IMonitoringScheduler, MonitoringScheduler>();

            await scheduler.Start();

            monitoringScheduler.AddJobRunOnce<HelloJob>("start", "start");
        }
    }
}
