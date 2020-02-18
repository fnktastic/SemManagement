using GalaSoft.MvvmLight;
using SemManagement.UWP.Collection;
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
        private readonly Playlist _playlist;
        private IEnumerable<Station> _originStations;
        private readonly IStationService _stationService;
        private readonly ILocalDataService _localDataService;
        #endregion

        #region properties
        private StationsCollection _selectedStations;
        public StationsCollection SelectedStations
        {
            get { return _selectedStations; }
            set
            {
                if (_selectedStations == value) return;
                _selectedStations = value;
                RaisePropertyChanged(nameof(SelectedStations));
            }
        }

        private StationsCollection _stations;
        public StationsCollection Stations
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
        public SendToStationViewModel(IStationService stationService, ILocalDataService localDataService, Playlist playlist)
        {
            _playlist = playlist;
            _stationService = stationService;
            _localDataService = localDataService;

            SelectedStations = new StationsCollection();

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

                    Stations = new StationsCollection(stations);

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

                Stations = new StationsCollection(part);
            }

            StaticSettings.StopSelectionChangedEvent = false;
        }

        private async void LoadData()
        {
            try
            {
                IsLoading = true;

                _originStations = (await _stationService.TakeAsync(int.MaxValue)).OrderBy(x => x, new StationComparer());

                var playlistStations = await _localDataService.GetStationsByPlaylist(_playlist.Plid);

                foreach(var _originStation in _originStations)
                {
                    if (playlistStations.Any(x => x.Sid == _originStation.Sid))
                        _originStation.IsAssigned = _originStation.IsSelected = true;
                }

                Stations = new StationsCollection(_originStations);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion

    }
}
