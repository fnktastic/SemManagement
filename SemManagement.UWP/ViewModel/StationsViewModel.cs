using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Collection;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.PlaylistModule.Service;
using SemManagement.UWP.Services.SongModule.Service;
using SemManagement.UWP.Services.StationModule.Service;
using SemManagement.UWP.View.ContentDialogs;
using SemManagement.UWP.ViewModel.ContentDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SemManagement.UWP.ViewModel
{
    public class StationsViewModel : ViewModelBase
    {
        #region fields
        private readonly ISongService _songService;
        private readonly IPlaylistService _playlistService;
        private readonly IStationService _stationService;
        private readonly ILocalDataService _localDataService;
        private List<Station> _originStations;
        #endregion

        #region properties
        private Stationsstatus _stationsstatus;
        public Stationsstatus Stationsstatus
        {
            get { return _stationsstatus; }
            set
            {
                if (_stationsstatus == value) return;
                _stationsstatus = value;
                RaisePropertyChanged(nameof(Stationsstatus));
            }
        }

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

                RefreshData();
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

        private ObservableCollection<SongExtended> _songsDeleted;
        public ObservableCollection<SongExtended> SongsDeleted
        {
            get { return _songsDeleted; }
            set
            {
                if (value == _songsDeleted) return;
                _songsDeleted = value;
                RaisePropertyChanged(nameof(SongsDeleted));
            }
        }

        private ObservableCollection<SongExtended> _songs;
        public ObservableCollection<SongExtended> Songs
        {
            get { return _songs; }
            set
            {
                if (_songs == value) return;
                _songs = value;
                RaisePropertyChanged(nameof(Songs));
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

        private ObservableCollection<StationQueue> _stationQueues;
        public ObservableCollection<StationQueue> StationQueues
        {
            get { return _stationQueues; }
            set
            {
                if (value == _stationQueues) return;
                _stationQueues = value;
                RaisePropertyChanged(nameof(StationQueues));
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

        private TagsCollection _tags;
        public TagsCollection Tags
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

                RefreshData();
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

                RefreshData();
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

                RefreshData();
            }
        }

        private bool _isDeletedSongs;
        public bool IsDeletedSongs
        {
            get { return _isDeletedSongs; }
            set
            {
                if (value == _isDeletedSongs) return;
                _isDeletedSongs = value;
                RaisePropertyChanged(nameof(IsDeletedSongs));

                RefreshData();
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

                RefreshData();
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

                RefreshData();
            }
        }

        private bool _isSchedulingTabSelected = false;
        public bool IsSchedulingTabSelected
        {
            get { return _isSchedulingTabSelected; }
            set
            {
                if (value == _isSchedulingTabSelected) return;
                _isSchedulingTabSelected = value;
                RaisePropertyChanged(nameof(IsSchedulingTabSelected));

                RefreshData();
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

        private string _newTag;
        public string NewTag
        {
            get { return _newTag; }
            set
            {
                if (value == _newTag) return;
                _newTag = value;
                RaisePropertyChanged(nameof(NewTag));
            }
        }

        private string _stationsFilterSearchTerm;
        public string StationsFilterSearchTerm
        {
            get { return _stationsFilterSearchTerm; }
            set
            {
                if (value == _stationsFilterSearchTerm) return;
                _stationsFilterSearchTerm = value;
                RaisePropertyChanged(nameof(StationsFilterSearchTerm));

                Filter_Stations();
            }
        }
        #endregion

        #region commands
        private RelayCommand _addNewTagCommand;
        public RelayCommand AddNewTagCommand => _addNewTagCommand ?? (_addNewTagCommand = new RelayCommand(AddNewTag));
        private async void AddNewTag()
        {
            try
            {
                IsDataLoading = true;

                if (string.IsNullOrWhiteSpace(_newTag)) return;

                var tags = _newTag
                    .Split(',')
                    .Select(x => x.Trim())
                    .Select(x => new Model.Local.Storage.Tag(Guid.NewGuid(), x))
                    .ToList();

                _tags.AddRange(tags);

                await _localDataService.SaveStationTagRangeAsync(_selectedStation, _tags.ToList());

                NewTag = string.Empty;
            }
            finally
            {
                IsDataLoading = false;
            }
        }


        private RelayCommand _loadSongsCommand;
        public RelayCommand LoadSongsCommand => _loadSongsCommand ?? (_loadSongsCommand = new RelayCommand(LoadSongs));
        private async void LoadSongs()
        {
            try
            {
                IsDataLoading = true;

                var songsDeleted = await _stationService.GetDeletedSongsAsync(_selectedStation.Sid);

                var songs = await _stationService.GetStationSongsAsync(_selectedStation.Sid);

                SongsDeleted = new ObservableCollection<SongExtended>(songsDeleted);

                Songs = new ObservableCollection<SongExtended>(songs);

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

                if (_selectedStation != null)
                {
                    var stationTags = await _localDataService.GetAllTagsAsync(_selectedStation.Sid);

                    Tags = new TagsCollection(stationTags);
                }
            }
            finally
            {
                IsDataLoading = false;
            }
        }

        private RelayCommand _loadStationQueueCommand;
        public RelayCommand LoadStationQueueCommand => _loadStationQueueCommand ?? (_loadStationQueueCommand = new RelayCommand(LoadStationQueue));
        private async void LoadStationQueue()
        {
            try
            {
                IsDataLoading = true;

                var stationQueues = (await _stationService.GetStationQueueAsync(_selectedStation.Sid))
                    .OrderByDescending(x => x.creation_Date)
                    .ToList();

                StationQueues = new ObservableCollection<StationQueue>(stationQueues);
            }
            finally
            {
                IsDataLoading = false;
            }
        }

        private RelayCommand _loadStationStatusCommand;
        public RelayCommand LoadStationStatusCommand => _loadStationStatusCommand ?? (_loadStationStatusCommand = new RelayCommand(LoadStationStatus));
        private async void LoadStationStatus()
        {
            try
            {
                IsDataLoading = true;

                var stationStatus = await _stationService.GetStationStatuses(_selectedStation.Sid);

                Stationsstatus = stationStatus;
            }
            finally
            {
                IsDataLoading = false;
            }
        }

        private RelayCommand<Playlist> _sendToStationCommand;
        public RelayCommand<Playlist> SendToStationCommand => _sendToStationCommand ?? (_sendToStationCommand = new RelayCommand<Playlist>(SendToStation));
        private async void SendToStation(Playlist playlist)
        {
            try
            {
                IsDataLoading = true;

                var sendToStationViewModel = new SendToStationViewModel(_stationService, _localDataService);

                var sendToStationContentDialog = new SendToStationContentDialog(sendToStationViewModel);

                var decision = await sendToStationContentDialog.ShowAsync();

                switch(decision)
                {
                    case ContentDialogResult.Primary:

                        foreach(var station in sendToStationViewModel.SelectedStations)
                        {
                            await _playlistService.AddPlaylistToStationAsync(playlist.Plid, station.Sid);
                        }
                        break;
                }
            }
            finally
            {
                IsDataLoading = false;
            }
        }

        private RelayCommand<Playlist> _removePlaylistCommand;
        public RelayCommand<Playlist> RemovePlaylistCommand => _removePlaylistCommand ?? (_removePlaylistCommand = new RelayCommand<Playlist>(RemovePlaylist));
        private async void RemovePlaylist(Playlist playlist)
        {
            try
            {
                IsDataLoading = true;

                _playlists.Remove(playlist);

                await _playlistService.RemovePlaylistFromStationAsync(playlist.Plid, _selectedStation.Sid);
            }
            finally
            {
                IsDataLoading = false;
            }
        }

        private RelayCommand _schedulingTabOpenedCommand;
        public RelayCommand SchedulingTabOpenedCommand => _schedulingTabOpenedCommand ?? (_schedulingTabOpenedCommand = new RelayCommand(SchedulingTabOpened));
        private async void SchedulingTabOpened()
        {
            try
            {
                IsDataLoading = true;

                var stationScheduling = await _stationService.GetStationSchedule(_selectedStation.Sid);
            }
            finally
            {
                IsDataLoading = false;
            }
        }
        #endregion

        #region constructor
        public StationsViewModel(ISongService songService, IStationService stationService, IPlaylistService playlistService, ILocalDataService localDataService)
        {
            _songService = songService;
            _stationService = stationService;
            _playlistService = playlistService;
            _localDataService = localDataService;

            LoadData();

            IsUserDetailsTabSelected = false;
        }
        #endregion

        #region private methods
        private void Filter_Stations()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originStations != null)
            {
                IEnumerable<Station> part = null;

                if (string.IsNullOrWhiteSpace(_stationsFilterSearchTerm))
                    part = _originStations.OrderBy(x => x, new StationComparer());
                else
                    part = _originStations
                        .Where(x => x.Name.Contains(_stationsFilterSearchTerm, StringComparison.OrdinalIgnoreCase))
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
                var stations = await _stationService.TakeAsync(int.MaxValue);
                _originStations = stations.ToList();
                Stations = new ObservableCollection<Station>(stations);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void RefreshData()
        {
            if (_selectedStation != null)
            {
                IsStationSelected = true;
            }

            if (_selectedStation == null)
            {
                IsStationSelected = false;
                return;
            }

            if (_isStatsTabSelected)
                LoadStatsCommand.Execute(null);

            if (_isSongsTabSelected)
                LoadSongsCommand.Execute(null);

            if (_isPlaylistsTabSelected)
                LoadStationPlaylistsCommand.Execute(null);

            if (_isUserDetailsTabSelected)
            {
                LoadTagsCommand.Execute(null);
                LoadStationStatusCommand.Execute(null);
            }

            if (_isStationQueueTabSelected)
                LoadStationQueueCommand.Execute(null);

            if (_isSchedulingTabSelected)
            {
                SchedulingTabOpenedCommand.Execute(null);
            }
        }
        #endregion
    }
}
