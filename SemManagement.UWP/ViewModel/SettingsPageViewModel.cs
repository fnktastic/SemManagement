﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SemManagement.UWP.Helper;
using SemManagement.UWP.Model;
using SemManagement.UWP.Services.Local.Settings;
using SemManagement.UWP.Services.Monitoring.Service;
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
        private readonly IMonitoringService _monitoringService;
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

        private bool _runMonitoringIsInProgress = false;
        public bool RunMonitoringIsInProgress
        {
            get { return _runMonitoringIsInProgress; }
            set
            {
                if (value == _runMonitoringIsInProgress) return;
                _runMonitoringIsInProgress = value;
                RaisePropertyChanged(nameof(RunMonitoringIsInProgress));

            }
        }

        private string _notificationStationOffline;
        public string NotificationStationOffline
        {
            get { return _notificationStationOffline; }
            set
            {
                if (_notificationStationOffline == value) return;
                _notificationStationOffline = value;
                RaisePropertyChanged(nameof(NotificationStationOffline));
            }
        }

        private string _notificationStationWasntSynced;
        public string NotificationStationWasntSynced
        {
            get { return _notificationStationWasntSynced; }
            set
            {
                if (_notificationStationWasntSynced == value) return;
                _notificationStationWasntSynced = value;
                RaisePropertyChanged(nameof(NotificationStationWasntSynced));
            }
        }
        #endregion

        public SettingsPageViewModel(ISettingsService settingsService, IMonitoringService monitoringService)
        {
            _settingsService = settingsService;
            _monitoringService = monitoringService;

            LoadSettings();
        }

        private void LoadSettings()
        {
            DefaultPeriodOfMonitoring = _settingsService.LoadSetting(Const.Default_Period_Of_Monitoring);
            MinimalAmountOfUpdates = _settingsService.LoadSetting(Const.Minimal_Amount_Of_Updates);
            NotificationStationOffline = _settingsService.LoadSetting(Const.Notificatin_Station_Offline);
            NotificationStationWasntSynced = _settingsService.LoadSetting(Const.Notificatin_Station_Wasnt_Synced);
        }

        private RelayCommand _saveSettingsCommand;
        public RelayCommand SaveSettingsCommand => _saveSettingsCommand ?? (_saveSettingsCommand = new RelayCommand(SaveSettings));
        private void SaveSettings()
        {
            _settingsService.SaveSetting(Const.Default_Period_Of_Monitoring, _defaultPeriodOfMonitoring);
            _settingsService.SaveSetting(Const.Minimal_Amount_Of_Updates, _minimalAmountOfUpdates);
            _settingsService.SaveSetting(Const.Notificatin_Station_Offline, NotificationStationOffline);
            _settingsService.SaveSetting(Const.Notificatin_Station_Wasnt_Synced, NotificationStationWasntSynced);
        }

        private RelayCommand _runMonitoringNowCommand;
        public RelayCommand RunMonitoringNowCommand => _runMonitoringNowCommand ?? (_runMonitoringNowCommand = new RelayCommand(RunMonitoringNow));
        private async void RunMonitoringNow()
        {
            RunMonitoringIsInProgress = true;

            var result = await _monitoringService.RunMonitoringNow();

            RunMonitoringIsInProgress = false;
        }

        private RelayCommand _startMonitoringCommand;
        public RelayCommand StartMonitoringCommand => _startMonitoringCommand ?? (_startMonitoringCommand = new RelayCommand(StartMonitoring));
        private async void StartMonitoring()
        {
            var result = await _monitoringService.StartMonitoring();
        }
    }
}
