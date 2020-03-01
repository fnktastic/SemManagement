using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Collection;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.Monitoring.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.ViewModel
{
    public class FeedViewModel : ViewModelBase
    {
        #region fields
        private readonly ILocalDataService _localDataService;
        #endregion

        #region properties
        private FeedCollection _feedItems;
        public FeedCollection FeedItems
        {
            get { return _feedItems; }
            set
            {
                if (value == _feedItems) return;
                _feedItems = value;
                RaisePropertyChanged(nameof(FeedItems));
            }

        }

        private bool _isDataLoading = false;
        public bool IsDataLoading
        {
            get { return _isDataLoading; }
            set
            {
                if (value == _isDataLoading) return;
                _isDataLoading = value;
                RaisePropertyChanged(nameof(IsDataLoading));

            }
        }
        #endregion

        #region constructor
        public FeedViewModel(ILocalDataService localDataService)
        {
            _localDataService = localDataService;

            LoadData();
        }
        #endregion

        #region private methods
        private async void LoadData()
        {
            try
            {
                IsDataLoading = true;

                LoadFastMonitoring();

                await Task.Delay(2500);
            }
            finally
            {
                IsDataLoading = false;
            }
        }
        #endregion

        #region commands
        private RelayCommand _loadFastMonitoringCommand;
        public RelayCommand LoadFastMonitoringCommand => _loadFastMonitoringCommand ?? (_loadFastMonitoringCommand = new RelayCommand(LoadFastMonitoring));
        private async void LoadFastMonitoring()
        {
            try
            {
                IsDataLoading = true;

                var dateTime = DateTime.Now.AddDays(-1);

                var fastMonitoring = (await _localDataService.GetQucikMonitoring(dateTime))
                    .OrderByDescending(x => x.DateTime)
                    .GroupBy(y => y.DateTime)
                    .ToList();

                FeedItems = new FeedCollection(fastMonitoring);
            }
            finally
            {
                IsDataLoading = false;
            }
        }
        #endregion
    }
}
