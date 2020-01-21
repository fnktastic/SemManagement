using GalaSoft.MvvmLight;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.StationModule.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.ViewModel.ContentDialog
{
    public class SendToStationViewModel : ViewModelBase
    {
        #region fields
        private IEnumerable<Station> _originStations;
        private readonly IStationService _stationService;
        private readonly ILocalDataService _localDataService;
        #endregion

        #region properties
        private ObservableCollectionFast<Station> _selectedStations;
        public ObservableCollectionFast<Station> SelectedStations
        {
            get { return _selectedStations; }
            set
            {
                if (_selectedStations == value) return;
                _selectedStations = value;
                RaisePropertyChanged(nameof(SelectedStations));
            }
        }

        private ObservableCollection<Station> _stations;
        public ObservableCollection<Station> Stations
        {
            get { return _stations; }
            set
            {
                if (_stations == value) return;
                _stations = value;
                RaisePropertyChanged(nameof(Stations));
            }
        }

        private string _stationsSearchTerm;
        public string StationsSearchTerm
        {
            get { return _stationsSearchTerm; }
            set
            {
                if (_stationsSearchTerm == value) return;
                _stationsSearchTerm = value;
                RaisePropertyChanged(nameof(StationsSearchTerm));

                Filter_Stations();
            }
        }

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
        #endregion

        #region constructor
        public SendToStationViewModel(IStationService stationService, ILocalDataService localDataService)
        {
            _stationService = stationService;
            _localDataService = localDataService;

            SelectedStations = new ObservableCollectionFast<Station>();

            LoadData();
        }
        #endregion

        #region private methods
        private async void Filter_Stations()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originStations != null)
            {
                if(_stationsSearchTerm.StartsWith("#"))
                {
                    var tags = _stationsSearchTerm.Replace("#", "").Split(",")
                        .Select(x => new Model.Local.Storage.Tag(x.Trim()))
                        .ToList();

                    var stations = await _localDataService.GetStationByTagsAsync(tags);

                    Stations = new ObservableCollectionFast<Station>(stations);

                    StaticSettings.StopSelectionChangedEvent = false;

                    return;
                }


                IEnumerable<Station> part = null;

                if (string.IsNullOrWhiteSpace(_stationsSearchTerm))
                    part = _originStations.OrderBy(x => x, new StationComparer());
                else
                    part = _originStations
                        .Where(x => x.Name.Contains(_stationsSearchTerm, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(x => x, new StationComparer());

                Stations = new ObservableCollectionFast<Station>(part);
            }

            StaticSettings.StopSelectionChangedEvent = false;
        }

        private async void LoadData()
        {
            try
            {
                IsLoading = true;

                _originStations = (await _stationService.TakeAsync(int.MaxValue)).OrderBy(x => x, new StationComparer());

                Stations = new ObservableCollection<Station>(_originStations);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion

    }
}
