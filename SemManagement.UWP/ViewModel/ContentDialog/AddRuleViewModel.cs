using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

        private ObservableCollection<Playlist> _sourcePlaylists;
        public ObservableCollection<Playlist> SourcePlaylists
        {
            get { return _sourcePlaylists; }
            set
            {
                if (_sourcePlaylists == value) return;
                _sourcePlaylists = value;
                RaisePropertyChanged(nameof(SourcePlaylists));
            }
        }

        private ObservableCollection<Playlist> _selectedSourcePlaylists;
        public ObservableCollection<Playlist> SelectedSourcePlaylists
        {
            get { return _selectedSourcePlaylists; }
            set
            {
                if (_selectedSourcePlaylists == value) return;
                _selectedSourcePlaylists = value;
                RaisePropertyChanged(nameof(SelectedSourcePlaylists));
            }
        }

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
        #endregion

        public AddRuleViewModel(IPlaylistService playlistService, IStationService stationService, ILocalDataService localDataService)
        {
            _playlistService = playlistService;
            _stationService = stationService;
            _localDataService = localDataService;

            SelectedSourcePlaylists = new ObservableCollection<Playlist>();

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

                var playlists = await _playlistService.TakeAsync(int.MaxValue);
                var stations = await _stationService.TakeAsync(int.MaxValue);

                SourcePlaylists = new ObservableCollection<Playlist>(playlists);
                TargetPlaylists = new ObservableCollection<Playlist>(playlists);
                Stations = new ObservableCollection<Station>(stations);
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
