using SemManagement.MonitoringContext.Model;
using SemManagement.MonitoringContext.Repository;
using SemManagement.MonitoringContext.Scheduler;
using SemManagement.MonitoringContext.Scheduler.Jobs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.MonitoringContext.Services
{
    public interface ISchedulerService
    {
        Task ScheduleMonitoring(StationMonitoring stationMonitoring);
    }
    public class SchedulerService : ISchedulerService
    {
        private readonly IMonitoringRepositry _monitoringRepositry;
        private readonly MonitoringScheduler _monitoringScheduler;

        public SchedulerService(MonitoringScheduler monitoringScheduler, IMonitoringRepositry monitoringRepositry)
        {
            _monitoringScheduler = monitoringScheduler;
            _monitoringRepositry = monitoringRepositry;
        }

        public async Task ScheduleMonitoring(StationMonitoring stationMonitoring)
        {
            await _monitoringRepositry.AddMonitoringStation(stationMonitoring);
        }
    }
}
