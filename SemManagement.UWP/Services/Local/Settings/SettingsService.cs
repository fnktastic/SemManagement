using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SemManagement.UWP.Services.Local.Settings
{
    public interface ISettingsService
    {
        void SaveSetting(string key, string value);
        string LoadSetting(string key);
    }

    public class SettingsService : ISettingsService
    {
        private static readonly ApplicationDataContainer _localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public string LoadSetting(string key)
        {
            return _localSettings.Values[key] as string;
        }

        public void SaveSetting(string key, string value)
        {
            _localSettings.Values[key] = value;
        }
    }
}
