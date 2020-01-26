using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.Monitoring.Service;

namespace SemManagement.UWP.ViewModel
{
    public class MonitoringViewModel : ViewModelBase
    {
        private readonly ILocalDataService _localDataService;
        private readonly IMonitoringService _monitoringService;

        public MonitoringViewModel(ILocalDataService localDataService, IMonitoringService monitoringService)
        {
            _localDataService = localDataService;
            _monitoringService = monitoringService;

            _monitoringService.AddMonitoringAsync(new Model.Monitoring()
            {
                Id = Guid.NewGuid(),
                StationId = 138,
                WantedAmountOfUpdates = 10,
                StartDateTime = DateTime.UtcNow,
                RepeatInterval = 5
            });
        }
    }
}
