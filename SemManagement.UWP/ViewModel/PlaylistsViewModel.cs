using GalaSoft.MvvmLight;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.PlaylistModule.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.ViewModel
{
    public class PlaylistsViewModel : ViewModelBase
    {
        #region fields
        private readonly IPlaylistService _playlistService;
        #endregion

        #region properties
        private ObservableCollection<Playlist> _playlists;
        public ObservableCollection<Playlist> Playlists
        {
            get { return _playlists; }
            set
            {
                if (value == _playlists) return;
                _playlists = value;
                RaisePropertyChanged(nameof(Playlists));
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

        public PlaylistsViewModel(IPlaylistService playlistService)
        {
            _playlistService = playlistService;

            LoadData();
        }

        #region private methods
        private async void LoadData()
        {
            try
            {
                IsLoading = true;
                var playlists = await _playlistService.TakeAsync(100);
                Playlists = new ObservableCollection<Playlist>(playlists);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }
}
