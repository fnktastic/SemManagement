using Quartz;
using SemManagement.MonitoringContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Scheduler.Jobs
{
    public class StartMonitoringStationsJob : IJob
    {
        private readonly IMonitoringService _monitoringService;

        public StartMonitoringStationsJob(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _monitoringService.MonitorAllActiveStations();
        }
    }
}
