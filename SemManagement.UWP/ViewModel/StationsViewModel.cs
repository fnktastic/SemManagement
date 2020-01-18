using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.PlaylistModule.Service;
using SemManagement.UWP.Services.SongModule.Service;
using SemManagement.UWP.Services.StationModule.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.ViewModel
{
    public class StationsViewModel : ViewModelBase
    {
        #region fields
        private readonly ISongService _songService;
        private readonly IPlaylistService _playlistService;
        private readonly IStationService _stationService;
        #endregion

        #region properties
        private bool _isStationSelected;
        public bool IsStationSelected
        {
            get { return _isStationSelected; }
            set
            {
                if (value == _isStationSelected) return;
                _isStationSelected = value;
                RaisePropertyChanged(nameof(IsStationSelected));

            }
        }

        private Station _selectedStation;
        public Station SelectedStation
        {
            get { return _selectedStation; }
            set
            {
                if (value == _selectedStation) return;
                _selectedStation = value;
                RaisePropertyChanged(nameof(SelectedStation));

                if (_selectedStation != null)
                {
                    IsStationSelected = true;
                }

                if (_selectedStation == null)
                {
                    IsStationSelected = false;
                }

                if (_isStatsTabSelected)
                    LoadStatsCommand.Execute(null);

                if (_isDeletedSongsTabSelected)
                    LoadDeletedSongsCommand.Execute(null);

                if (_isPlaylistsTabSelected)
                    LoadStationPlaylistsCommand.Execute(null);

                if (_isUserDetailsTabSelected)
                    LoadTagsCommand.Execute(null);
            }
        }

        private ObservableCollection<Station> _stations;
        public ObservableCollection<Station> Stations
        {
            get { return _stations; }
            set
            {
                if (value == _stations) return;
                _stations = value;
                RaisePropertyChanged(nameof(Stations));
            }
        }

        private ObservableCollection<SongsDeleted> _songsDeleted;
        public ObservableCollection<SongsDeleted> SongsDeleted
        {
            get { return _songsDeleted; }
            set
            {
                if (value == _songsDeleted) return;
                _songsDeleted = value;
                RaisePropertyChanged(nameof(SongsDeleted));
            }
        }

        private ObservableCollection<Song> _mostPopularSongs;
        public ObservableCollection<Song> MostPopularSongs
        {
            get { return _mostPopularSongs; }
            set
            {
                if (value == _mostPopularSongs) return;
                _mostPopularSongs = value;
                RaisePropertyChanged(nameof(MostPopularSongs));
            }

        }

        private ObservableCollection<Playlist> _playlists;
        public ObservableCollection<Playlist> Playlists
        {
            get { return _playlists; }
            set
            {
                if (_playlists == value) return;
                _playlists = value;
                RaisePropertyChanged(nameof(Playlists));
            }
        }

        private ObservableCollection<Model.Local.Storage.Tag> _tags;
        public ObservableCollection<Model.Local.Storage.Tag> Tags
        {
            get { return _tags; }
            set
            {
                if (value == _tags) return;
                _tags = value;
                RaisePropertyChanged(nameof(Tags));
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

        private bool _isUserDetailsTabSelected = false;
        public bool IsUserDetailsTabSelected
        {
            get { return _isUserDetailsTabSelected; }
            set
            {
                if (value == _isUserDetailsTabSelected) return;
                _isUserDetailsTabSelected = value;
                RaisePropertyChanged(nameof(IsUserDetailsTabSelected));

            }
        }

        private bool _isDeletedSongsTabSelected = false;
        public bool IsDeletedSongsTabSelected
        {
            get { return _isDeletedSongsTabSelected; }
            set
            {
                if (value == _isDeletedSongsTabSelected) return;
                _isDeletedSongsTabSelected = value;
                RaisePropertyChanged(nameof(IsDeletedSongsTabSelected));

            }
        }

        private bool _isPlaylistsTabSelected = false;
        public bool IsPlaylistsTabSelected
        {
            get { return _isPlaylistsTabSelected; }
            set
            {
                if (value == _isPlaylistsTabSelected) return;
                _isPlaylistsTabSelected = value;
                RaisePropertyChanged(nameof(IsPlaylistsTabSelected));

            }
        }

        private bool _isSongsTabSelected = false;
        public bool IsSongsTabSelected
        {
            get { return _isSongsTabSelected; }
            set
            {
                if (value == _isSongsTabSelected) return;
                _isSongsTabSelected = value;
                RaisePropertyChanged(nameof(IsSongsTabSelected));

            }
        }

        private bool _isStationQueueTabSelected = false;
        public bool IsStationQueueTabSelected
        {
            get { return _isStationQueueTabSelected; }
            set
            {
                if (value == _isStationQueueTabSelected) return;
                _isStationQueueTabSelected = value;
                RaisePropertyChanged(nameof(IsStationQueueTabSelected));

            }
        }

        private bool _isStatsTabSelected = false;
        public bool IsStatsTabSelected
        {
            get { return _isStatsTabSelected; }
            set
            {
                if (value == _isStatsTabSelected) return;
                _isStatsTabSelected = value;
                RaisePropertyChanged(nameof(IsStatsTabSelected));

                LoadStatsCommand.Execute(null);
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

        #region commands
        private RelayCommand _loadDeletedSongsCommand;
        public RelayCommand LoadDeletedSongsCommand => _loadDeletedSongsCommand ?? (_loadDeletedSongsCommand = new RelayCommand(LoadDeletedSongs));
        private async void LoadDeletedSongs()
        {
            try
            {
                IsDataLoading = true;

                var songsDeleted = await _stationService.GetDeletedSongsAsync(_selectedStation.Sid);
                SongsDeleted = new ObservableCollection<SongsDeleted>(songsDeleted);
            }
            finally
            {
                IsDataLoading = false;
            }
        }

        private RelayCommand _loadStatsCommand;
        public RelayCommand LoadStatsCommand => _loadStatsCommand ?? (_loadStatsCommand = new RelayCommand(LoadStats));
        private async void LoadStats()
        {
            try
            {
                IsDataLoading = true;

                var mostPopularSongs = await _songService.MostPopularSongs(_selectedStation.Sid, 15);
                MostPopularSongs = new ObservableCollection<Song>(mostPopularSongs);
            }
            finally
            {
                IsDataLoading = false;
            }
        }

        private RelayCommand _loadStationPlaylistsCommand;
        public RelayCommand LoadStationPlaylistsCommand => _loadStationPlaylistsCommand ?? (_loadStationPlaylistsCommand = new RelayCommand(LoadStationPlaylists));
        private async void LoadStationPlaylists()
        {
            try
            {
                IsDataLoading = true;

                var stationPlaylists = await _playlistService.GetPlaylistsByStationAsync(_selectedStation.Sid);

                Playlists = new ObservableCollection<Playlist>(stationPlaylists);
            }
            finally
            {
                IsDataLoading = false;
            }
        }

        private RelayCommand _loadTagsCommand;
        public RelayCommand LoadTagsCommand => _loadTagsCommand ?? (_loadTagsCommand = new RelayCommand(LoadTags));
        private async void LoadTags()
        {
            try
            {
                IsDataLoading = true;

                var stationPlaylists = await _playlistService.GetPlaylistsByStationAsync(_selectedStation.Sid);

                Playlists = new ObservableCollection<Playlist>(stationPlaylists);
            }
            finally
            {
                IsDataLoading = false;
            }
        }
        #endregion

        #region constructor
        public StationsViewModel(ISongService songService, IStationService stationService, IPlaylistService playlistService)
        {
            _songService = songService;
            _stationService = stationService;
            _playlistService = playlistService;

            LoadData();

            IsUserDetailsTabSelected = false;
        }
        #endregion

        #region private methods
        private async void LoadData()
        {
            try
            {
                IsLoading = true;
                var stations = await _stationService.TakeAsync(100);
                Stations = new ObservableCollection<Station>(stations);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }
}
