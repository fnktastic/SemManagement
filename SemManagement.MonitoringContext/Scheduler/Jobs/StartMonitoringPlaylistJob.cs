using Quartz;
using SemManagement.MonitoringContext.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Scheduler.Jobs
{
    public class StartMonitoringPlaylistJob : IJob
    {
        private readonly IMonitoringService _monitoringService;

        public StartMonitoringPlaylistJob(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _monitoringService.MonitorPlaylists();
        }
    }
}
