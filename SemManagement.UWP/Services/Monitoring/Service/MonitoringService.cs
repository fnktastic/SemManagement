using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Monitoring.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.Services.Monitoring.Service
{
    public interface IMonitoringService
    {
        Task AddMonitoringAsync(Model.Monitoring monitoring);
        Task<List<Model.Monitoring>> GetMonitoredStations();
    }
    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringProvider _monitoringProvider;

        public MonitoringService(IMonitoringProvider monitoringProvider)
        {
            _monitoringProvider = monitoringProvider;
        }

        public Task AddMonitoringAsync(Model.Monitoring monitoring)
        {
            return _monitoringProvider.AddMonitoringAsync(monitoring);
        }

        public Task<List<Model.Monitoring>> GetMonitoredStations()
        {
            return _monitoringProvider.GetMonitoredStations();
        }
    }
}
