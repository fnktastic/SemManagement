using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemManagement.UWP.ViewModel
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region foelds
        private readonly ISettingsService _settingsService;
        #endregion

        #region properties
        private string _rulePeriod;
        public string RulePeriod
        {
            get { return _rulePeriod; }
            set
            {
                if (_rulePeriod == value) return;
                _rulePeriod = value;
                RaisePropertyChanged(nameof(RulePeriod));
            }
        }
        #endregion

        public SettingsPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            LoadSettings();
        }

        private void LoadSettings()
        {
            RulePeriod = _settingsService.LoadSetting("rulePeriod");
        }

        private RelayCommand _saveSettingsCommand;
        public RelayCommand SaveSettingsCommand => _saveSettingsCommand ?? (_saveSettingsCommand = new RelayCommand(SaveSettings));
        private void SaveSettings()
        {
            _settingsService.SaveSetting("rulePeriod", _rulePeriod);
        }
    }
}
