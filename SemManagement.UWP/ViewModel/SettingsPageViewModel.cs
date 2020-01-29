﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Helper;
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
        #region fields
        private readonly ISettingsService _settingsService;
        #endregion

        #region properties
        private string _minimalAmountOfUpdates;
        public string MinimalAmountOfUpdates
        {
            get { return _minimalAmountOfUpdates; }
            set
            {
                if (_minimalAmountOfUpdates == value) return;
                _minimalAmountOfUpdates = value;
                RaisePropertyChanged(nameof(MinimalAmountOfUpdates));
            }
        }

        private string _defaultPeriodOfMonitoring;
        public string DefaultPeriodOfMonitoring
        {
            get { return _defaultPeriodOfMonitoring; }
            set
            {
                if (_defaultPeriodOfMonitoring == value) return;
                _defaultPeriodOfMonitoring = value;
                RaisePropertyChanged(nameof(DefaultPeriodOfMonitoring));
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
            DefaultPeriodOfMonitoring = _settingsService.LoadSetting(Const.Default_Period_Of_Monitoring);
            MinimalAmountOfUpdates = _settingsService.LoadSetting(Const.Minimal_Amount_Of_Updates);
        }

        private RelayCommand _saveSettingsCommand;
        public RelayCommand SaveSettingsCommand => _saveSettingsCommand ?? (_saveSettingsCommand = new RelayCommand(SaveSettings));
        private void SaveSettings()
        {
            _settingsService.SaveSetting(Const.Default_Period_Of_Monitoring, _defaultPeriodOfMonitoring);
            _settingsService.SaveSetting(Const.Minimal_Amount_Of_Updates, _minimalAmountOfUpdates);
        }
    }
}
