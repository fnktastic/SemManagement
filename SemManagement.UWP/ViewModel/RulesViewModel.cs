using AutoMapper;
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

        private readonly IMapper _mapper;

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

                if (_selectedRule != null)
                    GetRuleLogsCommand.Execute(_selectedRule.Id);
            }
        }
        #endregion

        public RulesViewModel(ILocalDataService localDataService, IPlaylistService playlistService, IStationService stationService, IMapper mapper)
        {
            _localDataService = localDataService;
            _playlistService = playlistService;
            _stationService = stationService;
            _mapper = mapper;

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

                SelectedRule = new Rule();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private Rule BuildRule(AddRuleViewModel addRuleViewModel, bool isDraft = false)
        {
            return new Rule()
            {
                Id = Guid.NewGuid(),
                Name = string.IsNullOrWhiteSpace(addRuleViewModel.RuleName) ? DateTime.Now.ToString() : addRuleViewModel.RuleName,
                Created = DateTime.UtcNow,
                IsDraft = isDraft,
                IsRepeat = addRuleViewModel.IsRepeat,
                AllStations = addRuleViewModel.AllStations,
                SourcePlaylists = _mapper.Map<List<Playlist>>(addRuleViewModel.SelectedSourcePlaylists),
                TargetPlaylists = _mapper.Map<List<Playlist>>(addRuleViewModel.SelectedTargetPlaylists),
                Stations = _mapper.Map<List<Station>>(addRuleViewModel.SelectedStations)
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

            Rule rule = null;

            switch (descision)
            {
                case ContentDialogResult.Primary:
                    rule = BuildRule(addRuleViewModel);
                    break;
                case ContentDialogResult.Secondary:
                    rule = BuildRule(addRuleViewModel, true);
                    break;
                default:
                    return;
            }

            _originRules.Add(rule);

            Rules.Add(rule);

            await _localDataService.SaveRuleAsync(rule);
        }

        private RelayCommand _fireRuleCommand;
        public RelayCommand FireRuleCommand => _fireRuleCommand ?? (_fireRuleCommand = new RelayCommand(FireRule));
        private async void FireRule()
        {
            _selectedRule.IsRuleInProcess = true;

            await _localDataService.FireRule(_selectedRule.Id);

            _selectedRule.IsRuleInProcess = false;
        }

        private RelayCommand<Guid> _getRuleLogsCommand;
        public RelayCommand<Guid> GetRuleLogsCommand => _getRuleLogsCommand ?? (_getRuleLogsCommand = new RelayCommand<Guid>(GetRuleLogs));
        private async void GetRuleLogs(Guid ruleId)
        {
            if (ruleId != Guid.Empty)
            {
                _selectedRule.IsRuleInProcess = true;
                var ruleLogs = await _localDataService.GetRuleLogs(ruleId);
                _selectedRule.IsRuleInProcess = false;
            }
        }
        #endregion
    }
}
