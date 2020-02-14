using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.StationModule.Service;
using System.Collections.ObjectModel;
using SemManagement.UWP.Collection;
using SemManagement.UWP.Services.PlaylistModule.Service;

namespace SemManagement.UWP.ViewModel.ContentDialog
{
    public class SendToPlaylistViewModel : ViewModelBase
    {
        #region fields
        private IList<Playlist> _originPlaylists;
        private readonly IPlaylistService _playlistService;
        private readonly ILocalDataService _localDataService;
        #endregion

        #region properties
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

        private PlaylistsCollection _playlists;
        public PlaylistsCollection Playlists
        {
            get { return _playlists; }
            set
            {
                if (_playlists == value) return;
                _playlists = value;
                RaisePropertyChanged(nameof(Playlists));
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
        public SendToPlaylistViewModel(IPlaylistService playlistService, ILocalDataService localDataService)
        {
            _playlistService = playlistService;
            _localDataService = localDataService;

            SelectedPlaylists = new PlaylistsCollection();

            LoadData();
        }
        #endregion

        #region private methods
        private void Filter_Stations()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originPlaylists != null)
            {
                IList<Playlist> part = null;

                if (string.IsNullOrWhiteSpace(_playlistsSearchTerm))
                    part = _originPlaylists.OrderBy(x => x, new PlaylistsComparer()).ToList();
                else
                    part = _originPlaylists
                        .Where(x => x.Name.Contains(_playlistsSearchTerm, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(x => x, new PlaylistsComparer())
                        .ToList();

                Playlists = new PlaylistsCollection(part);
            }

            StaticSettings.StopSelectionChangedEvent = false;
        }

        private async void LoadData()
        {
            try
            {
                IsLoading = true;

                _originPlaylists = (await _playlistService.TakeAsync(int.MaxValue))
                    .OrderBy(x => x, new PlaylistsComparer())
                    .ToList();

                Playlists = new PlaylistsCollection(_originPlaylists);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion

    }
}
