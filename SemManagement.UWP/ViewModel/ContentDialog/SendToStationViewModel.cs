using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Collection;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.PlaylistModule.Service;
using SemManagement.UWP.Services.StationModule.Service;
using SemManagement.UWP.View.ContentDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SemManagement.UWP.ViewModel.ContentDialog
{
    public class SendToStationViewModel : ViewModelBase
    {
        #region fields
        private readonly Playlist _playlist;
        private IEnumerable<Station> _originStations;
        private readonly IStationService _stationService;
        private readonly ILocalDataService _localDataService;
        private readonly IPlaylistService _playlistService;
        #endregion

        #region properties
        private bool _isHighlight;
        public bool IsHighlight
        {
            get { return _isHighlight; }
            set
            {
                if (_isHighlight == value) return;
                _isHighlight = value;
                RaisePropertyChanged(nameof(IsHighlight));
            }
        }

        private bool _isUnHighlight;
        public bool IsUnHighlight
        {
            get { return _isUnHighlight; }
            set
            {
                if (_isUnHighlight == value) return;
                _isUnHighlight = value;
                RaisePropertyChanged(nameof(IsUnHighlight));
            }
        }

        private bool _isAllSelected;
        public bool IsAllSelected
        {
            get { return _isAllSelected; }
            set
            {
                if (_isAllSelected == value) return;
                _isAllSelected = value;
                RaisePropertyChanged(nameof(IsAllSelected));

                SelectAll(_isAllSelected);
            }
        }

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
        public SendToStationViewModel(IStationService stationService, ILocalDataService localDataService, IPlaylistService playlistService, Playlist playlist)
        {
            _playlist = playlist;
            _stationService = stationService;
            _localDataService = localDataService;
            _playlistService = playlistService;

            SelectedStations = new StationsCollection();

            LoadData();
        }
        #endregion

        #region private methods
        private void SelectAll(bool select)
        {
            foreach (var station in Stations)
                station.IsSelected = select;
        }

        private async void Filter_Stations()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originStations != null)
            {
                if (string.IsNullOrWhiteSpace(_stationsSearchTerm) == false && _stationsSearchTerm.StartsWith("#"))
                {
                    var tags = _stationsSearchTerm.Replace("#", "").Split(",")
                        .Select(x => new Model.Local.Storage.Tag(x.Trim()))
                        .ToList();

                    var datas = await _localDataService.GetStationByTagsAsync(tags);

                    var stations = _originStations.IntersectBy(datas, x => x.Sid, y => y.Sid).ToList();

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

                foreach (var _originStation in _originStations)
                {
                    if (playlistStations.Any(x => x.Sid == _originStation.Sid))
                        _originStation.IsAssigned = true;
                }

                Stations = new StationsCollection(_originStations);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion

        #region commands
        private RelayCommand _higlightAssignedCommand;
        public RelayCommand HiglightAssignedCommand => _higlightAssignedCommand ?? (_higlightAssignedCommand = new RelayCommand(HiglightAssigned));
        private void HiglightAssigned()
        {
            try
            {
                Stations = new StationsCollection(_originStations.Where(x => x.IsAssigned));

                IsHighlight = true;
                IsUnHighlight = false;
            }
            finally
            {

            }
        }

        private RelayCommand _higlightNotAssignedCommand;
        public RelayCommand HiglightNotAssignedCommand => _higlightNotAssignedCommand ?? (_higlightNotAssignedCommand = new RelayCommand(HiglightNotAssigned));
        private void HiglightNotAssigned()
        {
            try
            {
                Stations = new StationsCollection(_originStations.Where(x => x.IsAssigned == false));

                IsHighlight = false;
                IsUnHighlight = true;
            }
            finally
            {

            }
        }

        private RelayCommand _resetHighlightCommand;
        public RelayCommand ResetHighlightCommand => _resetHighlightCommand ?? (_resetHighlightCommand = new RelayCommand(ResetHighlight));
        private void ResetHighlight()
        {
            try
            {
                Filter_Stations();

                IsHighlight = false;
                IsUnHighlight = false;
            }
            finally
            {

            }
        }

        private RelayCommand<Station> _removePlatlistFromStationCommand;
        public RelayCommand<Station> RemovePlatlistFromStationCommand => _removePlatlistFromStationCommand ?? (_removePlatlistFromStationCommand = new RelayCommand<Station>(RemovePlatlistFromStation));
        private async void RemovePlatlistFromStation(Station station)
        {
            try
            {
                IsLoading = true;

                await _playlistService.RemovePlaylistFromStationAsync(_playlist.Plid, station.Sid);

                station.IsAssigned = false;

                await Task.Delay(1000);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }
}
