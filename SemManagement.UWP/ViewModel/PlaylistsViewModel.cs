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
    public class PlaylistsViewModel : ViewModelBase
    {
        #region fields
        private readonly IPlaylistService _playlistService;
        private readonly ISongService _songService;
        private readonly IStationService _stationService;
        private readonly ILocalDataService _localDataService;
        private IEnumerable<Playlist> _originPlaylists;
        private IEnumerable<Song> _originAudios;
        #endregion

        #region properties
        private PlaylistsCollection _playlists;
        public PlaylistsCollection Playlists
        {
            get { return _playlists; }
            set
            {
                if (value == _playlists) return;
                _playlists = value;
                RaisePropertyChanged(nameof(Playlists));
            }
        }

        private SongsCollection _songs;
        public SongsCollection Songs
        {
            get { return _songs; }
            set
            {
                if (_songs == value) return;
                _songs = value;
                RaisePropertyChanged(nameof(Songs));
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

        private Playlist _selectedPlaylist;
        public Playlist SelectedPlaylist
        {
            get { return _selectedPlaylist; }
            set
            {
                if (value == _selectedPlaylist) return;
                _selectedPlaylist = value;
                RaisePropertyChanged(nameof(SelectedPlaylist));

                if (_selectedPlaylist != null)
                {
                    AudiosSearchTerm = string.Empty;
                    SelectedSongs.Clear();
                    LoadPlaylistAudioCommand.Execute(null);
                }
            }
        }

        private string _playlistsSearchTerm;
        public string PlaylistsSearchTerm
        {
            get { return _playlistsSearchTerm; }
            set
            {
                if (_playlistsSearchTerm == value) return;
                _playlistsSearchTerm = value;
                RaisePropertyChanged(nameof(PlaylistsSearchTerm));

                Filter_Playlists();
            }
        }

        private PlaylistsCollection _selectedPlaylists;
        public PlaylistsCollection SelectedPlaylists
        {
            get { return _selectedPlaylists; }
            set
            {
                if (_selectedPlaylists == value) return;
                _selectedPlaylists = value;
                RaisePropertyChanged(nameof(SelectedPlaylists));
            }
        }

        private string _audiosSearchTerm;
        public string AudiosSearchTerm
        {
            get { return _audiosSearchTerm; }
            set
            {
                if (_audiosSearchTerm == value) return;
                _audiosSearchTerm = value;
                RaisePropertyChanged(nameof(AudiosSearchTerm));

                Filter_Audios();
            }
        }

        private SongsCollection _selectedSongs;
        public SongsCollection SelectedSongs
        {
            get { return _selectedSongs; }
            set
            {
                if (_selectedSongs == value) return;
                _selectedSongs = value;
                RaisePropertyChanged(nameof(SelectedSongs));
            }
        }

        private bool _reversePlaylistListEnabled;
        public bool ReversePlaylistListEnabled
        {
            get { return _reversePlaylistListEnabled; }
            set
            {
                if (_reversePlaylistListEnabled == value) return;
                _reversePlaylistListEnabled = value;
                RaisePropertyChanged(nameof(ReversePlaylistListEnabled));
            }
        }

        private RelayCommand _reversePlaylistListCommand;
        public RelayCommand ReversePlaylistListCommand => _reversePlaylistListCommand ?? (_reversePlaylistListCommand = new RelayCommand(ReversePlaylistList));
        private void ReversePlaylistList()
        {
            try
            {
                Playlists = new PlaylistsCollection(Playlists.Reverse().ToList(), false);
            }
            finally
            {
                _reversePlaylistListEnabled = !_reversePlaylistListEnabled;
            }
        }

        private bool _sortAlphabeticallyEnabled;
        public bool SortAlphabeticallyEnabled
        {
            get { return _sortAlphabeticallyEnabled; }
            set
            {
                if (_sortAlphabeticallyEnabled == value) return;
                _sortAlphabeticallyEnabled = value;
                RaisePropertyChanged(nameof(SortAlphabeticallyEnabled));
            }
        }

        private RelayCommand _sortAlphabeticallyCommand;
        public RelayCommand SortAlphabeticallyCommand => _sortAlphabeticallyCommand ?? (_sortAlphabeticallyCommand = new RelayCommand(SortAlphabetically));
        private void SortAlphabetically()
        {
            try
            {
                if (_sortAlphabeticallyEnabled == false)
                    Playlists = new PlaylistsCollection(Playlists.OrderByDescending(x => x, new PlaylistsComparer()).ToList(), false);

                if (_sortAlphabeticallyEnabled)
                    Playlists = new PlaylistsCollection(Playlists.OrderBy(x => x, new PlaylistsComparer()).ToList(), false);
            }
            finally
            {
                SortByDateTimeEnabled = false;
            }
        }

        private bool _sortByDateTimeEnabled;
        public bool SortByDateTimeEnabled
        {
            get { return _sortByDateTimeEnabled; }
            set
            {
                if (_sortByDateTimeEnabled == value) return;
                _sortByDateTimeEnabled = value;
                RaisePropertyChanged(nameof(SortByDateTimeEnabled));
            }
        }

        private RelayCommand _sortByDateTimeCommand;
        public RelayCommand SortByDateTimeCommand => _sortByDateTimeCommand ?? (_sortByDateTimeCommand = new RelayCommand(SortByDateTime));
        private void SortByDateTime()
        {
            try
            {
                if (_sortByDateTimeEnabled == false)
                    Playlists = new PlaylistsCollection(Playlists.OrderBy(x => x.Plid).ToList(), false);

                if (_sortByDateTimeEnabled)
                    Playlists = new PlaylistsCollection(Playlists.OrderByDescending(x => x.Plid).ToList(), false);
            }
            finally
            {
                SortAlphabeticallyEnabled = false;
            }
        }
        #endregion

        public PlaylistsViewModel(IPlaylistService playlistService, IStationService stationService, ILocalDataService localDataService, ISongService songService)
        {
            _playlistService = playlistService;
            _stationService = stationService;
            _localDataService = localDataService;
            _songService = songService;

            LoadData();
        }

        #region private methods
        private async void LoadData()
        {
            try
            {
                IsLoading = true;
                var playlists = await _playlistService.TakeAsync(int.MaxValue);
                _originPlaylists = playlists.ToList();
                Playlists = new PlaylistsCollection(playlists);
                SelectedPlaylists = new PlaylistsCollection(new List<Playlist>());
                SelectedSongs = new SongsCollection(new List<Song>());
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void Filter_Playlists()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originPlaylists != null)
            {
                IList<Playlist> part = null;

                if (string.IsNullOrWhiteSpace(_playlistsSearchTerm))
                    part = _originPlaylists.OrderBy(x => x, new PlaylistsComparer()).OrderBy(x => x.No).ToList();
                else
                    part = _originPlaylists
                        .Where(x => x.Name.Contains(_playlistsSearchTerm, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(x => x, new PlaylistsComparer()).ToList();

                Playlists = new PlaylistsCollection(part, false);
            }

            StaticSettings.StopSelectionChangedEvent = false;
        }

        private void Filter_Audios()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originAudios != null)
            {
                IList<Song> part = null;

                if (string.IsNullOrWhiteSpace(_audiosSearchTerm))
                    part = _originAudios.OrderBy(x => x, new SongsComparer()).ToList();
                else
                    part = _originAudios
                        .Where(x => (x.Artist + " " + x.Title).Contains(_audiosSearchTerm, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(x => x, new SongsComparer()).ToList();

                Songs = new SongsCollection(part);
            }

            StaticSettings.StopSelectionChangedEvent = false;
        }
        #endregion

        #region commands
        private RelayCommand<Playlist> _sendToStationCommand;
        public RelayCommand<Playlist> SendToStationCommand => _sendToStationCommand ?? (_sendToStationCommand = new RelayCommand<Playlist>(SendToStation));
        private async void SendToStation(Playlist playlist)
        {
            try
            {
                var sendToStationViewModel = new SendToStationViewModel(_stationService, _localDataService);

                var sendToStationContentDialog = new SendToStationContentDialog(sendToStationViewModel);

                var decision = await sendToStationContentDialog.ShowAsync();

                switch (decision)
                {
                    case ContentDialogResult.Primary:

                        foreach (var station in sendToStationViewModel.SelectedStations)
                        {
                            await _playlistService.AddPlaylistToStationAsync(playlist.Plid, station.Sid);
                        }
                        break;
                }
            }
            finally
            {

            }
        }

        private RelayCommand _loadPlaylistAudioCommand;
        public RelayCommand LoadPlaylistAudioCommand => _loadPlaylistAudioCommand ?? (_loadPlaylistAudioCommand = new RelayCommand(LoadPlaylistAudio));
        private async void LoadPlaylistAudio()
        {
            try
            {
                var songs = await _songService.GetSongsByPlaylistAsync(_selectedPlaylist.Plid);

                Songs = new SongsCollection(songs);

                _originAudios = songs.ToList();
            }
            finally
            {

            }
        }

        private RelayCommand<Playlist> _removePlaylistCommand;
        public RelayCommand<Playlist> RemovePlaylistCommand => _removePlaylistCommand ?? (_removePlaylistCommand = new RelayCommand<Playlist>(RemovePlaylist));
        private async void RemovePlaylist(Playlist playlist)
        {
            try
            {
                _playlists.Remove(playlist);

                //await _playlistService.RemovePlaylistFromStationAsync(playlist.Plid, _selectedStation.Sid);
            }
            finally
            {

            }
        }

        private RelayCommand<Song> _sendToPlaylistCommand;
        public RelayCommand<Song> SendToPlaylistCommand => _sendToPlaylistCommand ?? (_sendToPlaylistCommand = new RelayCommand<Song>(SendToPlaylist));
        private void SendToPlaylist(Song song)
        {
            try
            {

            }
            finally
            {

            }
        }
        #endregion
    }
}
