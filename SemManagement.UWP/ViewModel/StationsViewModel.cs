using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Model;
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

        private readonly IStationService _stationService;
        #endregion

        #region properties
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
                    ExpandDetails = true;
                    LoadDeletedSongsCommand.Execute(null);
                }

                if (_selectedStation == null)
                    ExpandDetails = false;

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

        private bool _isDeletedSongsLoading = false;
        public bool IsDeletedSongsLoading
        {
            get { return _isDeletedSongsLoading; }
            set
            {
                if (value == _isDeletedSongsLoading) return;
                _isDeletedSongsLoading = value;
                RaisePropertyChanged(nameof(IsDeletedSongsLoading));

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

        private bool _expandDetails = false;
        public bool ExpandDetails
        {
            get { return _expandDetails; }
            set
            {
                if (value == _expandDetails) return;
                _expandDetails = value;
                RaisePropertyChanged(nameof(ExpandDetails));

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
                IsDeletedSongsLoading = true;

                var songsDeleted = await _stationService.GetFakeDeletedSongsAsync(_selectedStation.Sid);
                SongsDeleted = new ObservableCollection<SongsDeleted>(songsDeleted);
            }
            finally
            {
                IsDeletedSongsLoading = false;
            }
        }
        #endregion

        #region constructor
        public StationsViewModel(ISongService songService, IStationService stationService)
        {
            _songService = songService;
            _stationService = stationService;

            LoadData();
        }
        #endregion

        #region private methods
        private async void LoadData()
        {
            try
            {
                IsLoading = true;

                var stations = await _stationService.GetFakeDataAsync();
                Stations = new ObservableCollection<Station>(stations);
            }
            finally
            {
                IsLoading = false;
            }

            //var stations = await _stationService.TakeAsync(100);

            //Stations = new ObservableCollection<Station>(stations);

            //var deletedSongs = await _stationService.GetDeletedSongsAsync(848);
        }
        #endregion
    }
}
