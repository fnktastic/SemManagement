using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.PlaylistModule.Service;
using SemManagement.UWP.Services.StationModule.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.UI.Xaml.Controls;

namespace SemManagement.UWP.ViewModel.ContentDialog
{
    public class AddRuleViewModel : ViewModelBase
    {
        #region fields
        private const int PAGE_SIZE = 20;

        private readonly IPlaylistService _playlistService;
        private readonly IStationService _stationService;
        private readonly ILocalDataService _localDataService;
        private IEnumerable<Playlist> _originPlaylists;
        private IEnumerable<Station> _originStations;
        #endregion

        #region properties
        public string Title => "Set up your rule";

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

        private bool _isRepeat;
        public bool IsRepeat
        {
            get { return _isRepeat; }
            set
            {
                if (_isRepeat == value) return;
                _isRepeat = value;
                RaisePropertyChanged(nameof(IsRepeat));
            }
        }

        private string _ruleName;
        public string RuleName
        {
            get { return _ruleName; }
            set
            {
                if (value == _ruleName) return;
                _ruleName = value;
                RaisePropertyChanged(nameof(RuleName));
            }
        }

        #region source
        private ObservableCollectionFast<Playlist> _sourcePlaylists;
        public ObservableCollectionFast<Playlist> SourcePlaylists
        {
            get { return _sourcePlaylists; }
            set
            {
                if (_sourcePlaylists == value) return;
                _sourcePlaylists = value;
                RaisePropertyChanged(nameof(SourcePlaylists));
            }
        }

        private string _sourcePlaylistsSearchTerm;
        public string SourcePlaylistsSearchTerm
        {
            get { return _sourcePlaylistsSearchTerm; }
            set
            {
                if (_sourcePlaylistsSearchTerm == value) return;
                _sourcePlaylistsSearchTerm = value;
                RaisePropertyChanged(nameof(SourcePlaylistsSearchTerm));

                Filter_SourcePlaylists();
            }
        }

        private ObservableCollectionFast<Playlist> _selectedSourcePlaylists;
        public ObservableCollectionFast<Playlist> SelectedSourcePlaylists
        {
            get { return _selectedSourcePlaylists; }
            set
            {
                if (_selectedSourcePlaylists == value) return;
                _selectedSourcePlaylists = value;
                RaisePropertyChanged(nameof(SelectedSourcePlaylists));
            }
        }

        private void Filter_SourcePlaylists()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originPlaylists != null)
            {
                IEnumerable<Playlist> part = null;

                if (string.IsNullOrWhiteSpace(_sourcePlaylistsSearchTerm))
                    part = _originPlaylists.OrderBy(x => x, new PlaylistsComparer());
                else
                    part = _originPlaylists
                        .Where(x => x.Name.Contains(_sourcePlaylistsSearchTerm, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(x => x, new PlaylistsComparer());

                SourcePlaylists = new ObservableCollectionFast<Playlist>(part);
            }

            StaticSettings.StopSelectionChangedEvent = false;
        }
        #endregion

        #region target
        private ObservableCollection<Playlist> _targetPlaylists;
        public ObservableCollection<Playlist> TargetPlaylists
        {
            get { return _targetPlaylists; }
            set
            {
                if (_targetPlaylists == value) return;
                _targetPlaylists = value;
                RaisePropertyChanged(nameof(TargetPlaylists));
            }
        }

        private string _targetPlaylistsSearchTerm;
        public string TargetPlaylistsSearchTerm
        {
            get { return _targetPlaylistsSearchTerm; }
            set
            {
                if (_targetPlaylistsSearchTerm == value) return;
                _targetPlaylistsSearchTerm = value;
                RaisePropertyChanged(nameof(TargetPlaylistsSearchTerm));

                Filter_TargetPlaylists();
            }
        }

        private ObservableCollectionFast<Playlist> _selectedTargetPlaylists;
        public ObservableCollectionFast<Playlist> SelectedTargetPlaylists
        {
            get { return _selectedTargetPlaylists; }
            set
            {
                if (_selectedTargetPlaylists == value) return;
                _selectedTargetPlaylists = value;
                RaisePropertyChanged(nameof(SelectedTargetPlaylists));
            }
        }

        private void Filter_TargetPlaylists()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originPlaylists != null)
            {
                IEnumerable<Playlist> part = null;

                if (string.IsNullOrWhiteSpace(_targetPlaylistsSearchTerm))
                    part = _originPlaylists.OrderBy(x => x, new PlaylistsComparer());
                else
                    part = _originPlaylists
                        .Where(x => x.Name.Contains(_targetPlaylistsSearchTerm, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(x => x, new PlaylistsComparer());

                TargetPlaylists = new ObservableCollectionFast<Playlist>(part);
            }

            StaticSettings.StopSelectionChangedEvent = false;
        }
        #endregion

        #region station
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

        private void Filter_Stations()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originPlaylists != null)
            {
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
        #endregion
        #endregion

        public AddRuleViewModel(IPlaylistService playlistService, IStationService stationService, ILocalDataService localDataService)
        {
            _playlistService = playlistService;
            _stationService = stationService;
            _localDataService = localDataService;

            SelectedSourcePlaylists = new ObservableCollectionFast<Playlist>();
            SelectedStations = new ObservableCollectionFast<Station>();
            SelectedTargetPlaylists = new ObservableCollectionFast<Playlist>();

            LoadData();
        }

        #region methods
        private async void LoadData()
        {
            try
            {
                IsLoading = true;

                var stationsTotalCount = await _stationService.CountAsync();
                var playlistsTotalCount = await _playlistService.CountAsync();

                _originPlaylists = (await _playlistService.TakeAsync(int.MaxValue)).OrderBy(x => x, new PlaylistsComparer());
                _originStations = (await _stationService.TakeAsync(int.MaxValue)).OrderBy(x => x, new StationComparer());

                SourcePlaylists = new ObservableCollectionFast<Playlist>(_originPlaylists);
                TargetPlaylists = new ObservableCollection<Playlist>(_originPlaylists);
                Stations = new ObservableCollection<Station>(_originStations);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion

        #region commands
        #endregion
    }
}
