using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Model.Local.Storage;
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
using Windows.UI.Xaml.Controls;

namespace SemManagement.UWP.ViewModel
{
    public class RulesViewModel : ViewModelBase
    {
        #region fields
        private readonly ILocalDataService _localDataService;

        private readonly IPlaylistService _playlistService;
        private readonly IStationService _stationService;

        private List<Rule> _originRules;
        #endregion

        #region properties
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

        private ObservableCollectionFast<Rule> _rules;
        public ObservableCollectionFast<Rule> Rules
        {
            get { return _rules; }
            set
            {
                if (_rules == value) return;
                _rules = value;
                RaisePropertyChanged(nameof(Rules));
            }
        }

        private string _rulesSearchTerm;
        public string RulesSearchTerm
        {
            get { return _rulesSearchTerm; }
            set
            {
                if (_rulesSearchTerm == value) return;
                _rulesSearchTerm = value;
                RaisePropertyChanged(nameof(RulesSearchTerm));

                Filter_Rules();
            }
        }

        private void Filter_Rules()
        {
            StaticSettings.StopSelectionChangedEvent = true;

            if (_originRules != null)
            {
                IEnumerable<Rule> part = null;

                if (string.IsNullOrWhiteSpace(_rulesSearchTerm))
                    part = _originRules.OrderBy(x => x, new RulesComparer());
                else
                    part = _originRules
                        .Where(x => x.Name.Contains(_rulesSearchTerm, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(x => x, new RulesComparer());

                Rules = new ObservableCollectionFast<Rule>(part);
            }

            StaticSettings.StopSelectionChangedEvent = false;
        }

        private Rule _selectedRule;
        public Rule SelectedRule
        {
            get { return _selectedRule; }
            set
            {
                if (value == _selectedRule) return;
                _selectedRule = value;
                RaisePropertyChanged(nameof(SelectedRule));
            }
        }
        #endregion

        public RulesViewModel(ILocalDataService localDataService, IPlaylistService playlistService, IStationService stationService)
        {
            _localDataService = localDataService;
            _playlistService = playlistService;
            _stationService = stationService;

            LoadData();
        }

        #region private methods
        private async void LoadData()
        {
            try
            {
                IsLoading = true;

                _originRules = await _localDataService.GetAllRulesAsync();

                Rules = new ObservableCollectionFast<Rule>(_originRules);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private Rule BuildRule(AddRuleViewModel addRuleViewModel)
        {
            return new Rule()
            {
                Name = addRuleViewModel.RuleName
            };
        }
        #endregion

        #region commands
        private RelayCommand _addRuleCommand;
        public RelayCommand AddRuleCommand => _addRuleCommand ?? (_addRuleCommand = new RelayCommand(AddRule));
        private async void AddRule()
        {
            var addRuleViewModel = new AddRuleViewModel(_playlistService, _stationService, _localDataService);

            var addRuleContentDialog = new AddRuleContentDialog(addRuleViewModel);

            var descision = await addRuleContentDialog.ShowAsync();

            switch (descision)
            {
                case ContentDialogResult.Primary:
                case ContentDialogResult.Secondary:
                    var rule = BuildRule(addRuleViewModel);
                    _originRules.Add(rule);
                    Rules.Add(rule);
                    break;
                case ContentDialogResult.None:
                    break;
            }
        }
        #endregion
    }
}
