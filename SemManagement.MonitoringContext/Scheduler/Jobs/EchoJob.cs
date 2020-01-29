using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Scheduler.Jobs
{
    public class EchoJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await GetAllJobs(context.Scheduler);
        }

        private static async Task GetAllJobs(IScheduler scheduler)
        {
            var executingJobs = await scheduler.GetCurrentlyExecutingJobs();
            foreach (var job in executingJobs)
            {
                Debug.WriteLine(job.JobDetail.Key);
            }
        }
    }
}
