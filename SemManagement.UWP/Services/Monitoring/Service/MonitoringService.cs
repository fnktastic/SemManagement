using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Monitoring.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemManagement.UWP.Model.Local.Storage;

namespace SemManagement.UWP.Services.Monitoring.Service
{
    public interface IMonitoringService
    {
        Task AddMonitoringAsync(Model.Local.Storage.Monitoring monitoring);
        Task<List<Model.Local.Storage.Monitoring>> GetMonitoredStations();
        Task<BoolResult> RunMonitoringNow();
    }
    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringProvider _monitoringProvider;

        public MonitoringService(IMonitoringProvider monitoringProvider)
        {
            _monitoringProvider = monitoringProvider;
        }

        public Task AddMonitoringAsync(Model.Local.Storage.Monitoring monitoring)
        {
            return _monitoringProvider.AddMonitoringAsync(monitoring);
        }

        public Task<List<Model.Local.Storage.Monitoring>> GetMonitoredStations()
        {
            return _monitoringProvider.GetMonitoredStations();
        }

        public Task<BoolResult> RunMonitoringNow()
        {
            return _monitoringProvider.RunMonitoringNow();
        }
    }
}
