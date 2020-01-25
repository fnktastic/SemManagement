using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SemManagement.API.Scheduler.Jobs;

namespace SemManagement.API.Scheduler
{
    public static class Extensions
    {
        public static async void AddQuartz(this IServiceCollection services)
        {
            services.AddTransient<HelloJob>();

            var container = services.BuildServiceProvider();

            var jobFactory = new JobFactory(container);

            var schedulerFactory = new StdSchedulerFactory();

            var scheduler = await schedulerFactory.GetScheduler();

            scheduler.JobFactory = jobFactory;

            services.AddSingleton<IScheduler>(scheduler);

            var monitoringScheduler = new MonitoringScheduler(scheduler);

            services.AddSingleton<MonitoringScheduler>(monitoringScheduler);

            await scheduler.Start();
        }
    }
}
