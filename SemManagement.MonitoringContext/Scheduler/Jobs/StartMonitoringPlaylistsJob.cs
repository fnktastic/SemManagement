using Quartz;
using SemManagement.MonitoringContext.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Scheduler.Jobs
{
    public class StartMonitoringPlaylistsJob : IJob
    {
        private readonly IMonitoringService _monitoringService;

        public StartMonitoringPlaylistsJob(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _monitoringService.MonitorPlaylists();
        }
    }
}
