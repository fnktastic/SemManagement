using Quartz;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Services;
using SemManagement.SemContext.Repository;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Scheduler.Jobs
{
    public class HelloJob : IJob
    {
        public HelloJob()
        {

        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => Debug.WriteLine("Start Monitor Stations"));
        }
    }
}
