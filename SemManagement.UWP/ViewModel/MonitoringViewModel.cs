using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.Monitoring.Service;
using System.Collections.ObjectModel;
using SemManagement.UWP.Model;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Services.StationModule.Service;
using SemManagement.UWP.Services.PlaylistModule.Service;
using SemManagement.UWP.ViewModel.ContentDialog;
using SemManagement.UWP.View.ContentDialogs;
using Windows.UI.Xaml.Controls;
using SemManagement.UWP.Model.Local.Storage;

namespace SemManagement.UWP.ViewModel
{
    public class MonitoringViewModel : ViewModelBase
    {
        #region fields
        private readonly ILocalDataService _localDataService;
        private readonly IMonitoringService _monitoringService;
        private readonly IStationService _stationService;
        private List<Monitoring> _originMonitorings;
        #endregion

        #region properties
        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == _isLoading) return;
                _isLoading = value;
                RaisePropertyChanged(nameof(IsLoading));

            }
        }

        private ObservableCollection<Monitoring> _monitorings;
        public ObservableCollection<Monitoring> Monitorings
        {
            get { return _monitorings; }
            set
            {
                if (_monitorings == value) return;
                _monitorings = value;
                RaisePropertyChanged(nameof(Monitorings));
            }
        }
        #endregion

        public MonitoringViewModel(ILocalDataService localDataService, IMonitoringService monitoringService, IStationService stationService)
        {
            _localDataService = localDataService;
            _monitoringService = monitoringService;
            _stationService = stationService;

            LoadData();
        }

        #region commands
        private RelayCommand _addMonitoringItemCommand;
        public RelayCommand AddMonitoringItemCommand => _addMonitoringItemCommand ?? (_addMonitoringItemCommand = new RelayCommand(AddMonitoringItem));
        private async void AddMonitoringItem()
        {
            try
            {
                var startMonitoringViewModel = new StartMonitoringViewModel(_stationService, _localDataService);

                var startMonitoringContentDialog = new StartMonitoringContentDialog(startMonitoringViewModel);

                var descision = await startMonitoringContentDialog.ShowAsync();

                var monitorings = new List<Monitoring>();

                switch (descision)
                {
                    case ContentDialogResult.Primary:
                        monitorings = BuildMonitorings(startMonitoringViewModel);
                        break;
                    case ContentDialogResult.Secondary:

                        break;
                    default:
                        return;
                }

                foreach(var monitoring in monitorings)
                {
                    _originMonitorings.Add(monitoring);

                    Monitorings.Add(monitoring);

                    await _monitoringService.AddMonitoringAsync(monitoring);
                }
            }
            finally
            {

            }
        }
        
        #endregion

        #region methods
        private List<Monitoring> BuildMonitorings(StartMonitoringViewModel startMonitoringViewModel)
        {
            var list = new List<Monitoring>();

            if(startMonitoringViewModel.SelectedStations != null && startMonitoringViewModel.SelectedStations.Count > 0)
            {
                foreach(var selectedStation in startMonitoringViewModel.SelectedStations)
                {
                    var monitor = new Monitoring()
                    {
                        Id = Guid.NewGuid(),
                        RepeatInterval = int.Parse(startMonitoringViewModel.RepeatInterval),
                        StartDateTime = startMonitoringViewModel.StartDateTime,
                        StationName = selectedStation.Name,
                        WantedAmountOfUpdates = int.Parse(startMonitoringViewModel.WantedAmountOfUpdates),
                        StationId = selectedStation.Sid
                    };

                    list.Add(monitor);
                }
            }

            return list;
        }

        private async void LoadData()
        {
            try
            {
                IsLoading = true;
                var monitoringItems = await _monitoringService.GetMonitoredStations();

                Monitorings = new ObservableCollection<Monitoring>(monitoringItems);

                _originMonitorings = monitoringItems.ToList();
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }
}
