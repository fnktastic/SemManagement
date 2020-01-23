using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using SemManagement.UWP.Services.Local.Storage;

namespace SemManagement.UWP.ViewModel
{
    public class MonitoringViewModel : ViewModelBase
    {
        private readonly ILocalDataService _localDataService;

        public MonitoringViewModel(ILocalDataService localDataService)
        {
            _localDataService = localDataService;
        }
    }
}
