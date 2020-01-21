using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Storage;
using SemManagement.UWP.Services.PlaylistModule.Service;
using SemManagement.UWP.Services.StationModule.Service;
using SemManagement.UWP.View.ContentDialogs;
using SemManagement.UWP.ViewModel.ContentDialog;
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
        private readonly IStationService _stationService;
        private readonly ILocalDataService _localDataService;
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

        public PlaylistsViewModel(IPlaylistService playlistService, IStationService stationService, ILocalDataService localDataService)
        {
            _playlistService = playlistService;
            _stationService = stationService;
            _localDataService = localDataService;

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

        #region commands
        private RelayCommand _sendToStationCommand;
        public RelayCommand SendToStationCommand => _sendToStationCommand ?? (_sendToStationCommand = new RelayCommand(SendToStation));
        private async void SendToStation()
        {
            try
            {
                var sendToStationViewModel = new SendToStationViewModel(_stationService, _localDataService);

                var sendToStationContentDialog = new SendToStationContentDialog(sendToStationViewModel);

                var descision = await sendToStationContentDialog.ShowAsync();
            }
            finally
            {

            }
        }
        #endregion
    }
}
