using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Collection;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.ViewModel.ContentDialog
{
    public class ManageTagsViewModel : ViewModelBase
    {
        #region private fields
        private readonly Playlist _playlist;
        private readonly ILocalDataService _localDataService;
        #endregion

        #region properties
        private TagsCollection _tags;
        public TagsCollection Tags
        {
            get { return _tags; }
            set
            {
                if (_tags == value) return;
                _tags = value;
                RaisePropertyChanged(nameof(Tags));
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

        //public string ContentDialogTitle => $"Playlist {_playlist.Name} #tags management";
        #endregion

        public ManageTagsViewModel(ILocalDataService localDataService, Playlist playlist)
        {
            _localDataService = localDataService;
            _playlist = playlist;

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                IsLoading = true;

                var playlistTags = await _localDataService.GetAllPlaylisTagsAsync(_playlist.Plid);

                Tags = new TagsCollection(playlistTags);

            }
            finally
            {
                IsLoading = false;
            }
        }

        #region commands
        private RelayCommand _addNewTagCommand;
        public RelayCommand AddNewTagCommand => _addNewTagCommand ?? (_addNewTagCommand = new RelayCommand(AddNewTag));
        private async void AddNewTag()
        {
            try
            {
                IsLoading = true;

                if (string.IsNullOrWhiteSpace(_newTag)) return;

                var tags = _newTag
                    .Split(',')
                    .Select(x => x.Trim())
                    .Select(x => new Model.Local.Storage.Tag(Guid.NewGuid(), x))
                    .ToList();

                _tags.AddRange(tags);

                await _localDataService.SavePlaylistTagRangeAsync(_playlist, tags.ToList());

                NewTag = string.Empty;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private RelayCommand<Model.Local.Storage.Tag> _removeTagCommand;
        public RelayCommand<Model.Local.Storage.Tag> RemoveTagCommand => _removeTagCommand ?? (_removeTagCommand = new RelayCommand<Model.Local.Storage.Tag>(RemoveTag));
        private async void RemoveTag(Model.Local.Storage.Tag tag)
        {
            try
            {
                IsLoading = true;

                var boolResult = await _localDataService.DeletePlaylistTagByIdAsync(_playlist.Plid, tag.Id);

                if (boolResult.Success)
                    _tags.Remove(tag);
            }
            finally
            {
                IsLoading = false;
            }
        }
        #endregion
    }
}
