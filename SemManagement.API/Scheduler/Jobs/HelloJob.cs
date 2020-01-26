using Quartz;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Services;
using SemManagement.SemContext.Repository;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SemManagement.API.Scheduler.Jobs
{
    public class HelloJob : Quartz.IJob
    {
        private readonly IMonitoringService _monitoringService;

        public HelloJob(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            await _monitoringService.MonitorPlaylists();

            await Task.Run(() => Debug.WriteLine(DateTime.UtcNow));
        }
    }
}
