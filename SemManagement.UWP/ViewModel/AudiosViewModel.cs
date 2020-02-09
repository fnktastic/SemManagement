using GalaSoft.MvvmLight;
using SemManagement.UWP.Collection;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.SongModule.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.ViewModel
{
    public class AudiosViewModel : ViewModelBase
    {
        #region fields
        private readonly ISongService _songService;
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
        #endregion

        public AudiosViewModel(ISongService songService)
        {
            _songService = songService;

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
    }
}
