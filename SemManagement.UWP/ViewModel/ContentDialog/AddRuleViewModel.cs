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
        private List<Playlist> _playlists;
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

            if (_playlists != null)
            {
                IEnumerable<Playlist> part = null;

                if (string.IsNullOrWhiteSpace(_sourcePlaylistsSearchTerm))
                    part = _playlists.OrderBy(x => x, new PlaylistsComparer());
                else
                    part = _playlists
                        .Where(x => x.Name.Contains(_sourcePlaylistsSearchTerm, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(x => x, new PlaylistsComparer());

                for (int i = SourcePlaylists.Count - 1; i >= 0; i--)
                {
                    var item = SourcePlaylists[i];

                    if (!part.Contains(item))
                    {
                        SourcePlaylists.Remove(item);
                    }
                }

                foreach (var item in part)
                {
                    if (!SourcePlaylists.Contains(item))
                    {
                        SourcePlaylists.Add(item);
                    }
                }
            }

            StaticSettings.StopSelectionChangedEvent = false;
        }
        #endregion

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

            SelectedSourcePlaylists = new ObservableCollectionFast<Playlist>();

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

                _playlists = await _playlistService.TakeAsync(int.MaxValue);
                var stations = await _stationService.TakeAsync(int.MaxValue);

                SourcePlaylists = new ObservableCollectionFast<Playlist>(_playlists);
                TargetPlaylists = new ObservableCollection<Playlist>(_playlists);
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
