using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Collection;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.PlaylistModule.Service;
using SemManagement.UWP.Services.SongModule.Service;
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
    public class AudiosViewModel : ViewModelBase
    {
        #region fields
        private readonly ISongService _songService;
        private readonly IPlaylistService _playlistService;
        private readonly ILocalDataService _localDataService;
        #endregion

        #region properties
        private Song _song;
        public Song Song
        {
            get { return _song; }
            set
            {
                if (value == _song) return;
                _song = value;
                RaisePropertyChanged(nameof(Song));
            }
        }

        private SongsCollection _songs;
        public SongsCollection Songs
        {
            get { return _songs; }
            set
            {
                if (value == _songs) return;
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

        public AudiosViewModel(ISongService songService, IPlaylistService playlistService, ILocalDataService localDataService)
        {
            _songService = songService;
            _playlistService = playlistService;
            _localDataService = localDataService;

            LoadData();
        }

        #region private methods
        private async void LoadData()
        {
            try
            {
                IsLoading = true;
                var songs = await _songService.TakeAsync(100);
                Songs = new SongsCollection(songs);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion

        #region commands
        private RelayCommand<Song> _sendToPlaylistCommand;
        public RelayCommand<Song> SendToPlaylistCommand => _sendToPlaylistCommand ?? (_sendToPlaylistCommand = new RelayCommand<Song>(SendToPlaylist));
        private async void SendToPlaylist(Song song)
        {
            try
            {
                IsDataLoading = true;

                var sendToStationViewModel = new SendToPlaylistViewModel(_playlistService, _localDataService);

                var sendToPlaylistContentDialog = new SendToPlaylistContentDialog(sendToStationViewModel);

                var decision = await sendToPlaylistContentDialog.ShowAsync();

                switch (decision)
                {
                    case ContentDialogResult.Primary:
                        await _playlistService.SendSongToPlaylistsAsync(song.Sgid, sendToStationViewModel.SelectedPlaylists.ToList());
                        break;
                }
            }
            finally
            {
                IsDataLoading = false;
            }
        }
        #endregion
    }
}
