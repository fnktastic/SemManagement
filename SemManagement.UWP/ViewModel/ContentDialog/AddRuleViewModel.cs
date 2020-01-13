using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SemManagement.UWP.ViewModel.ContentDialog
{
    public class AddRuleViewModel : ViewModelBase
    {
        #region properties
        public string Title => "Set up your rule";

        private bool _isOneTimeRule;
        public bool IsOneTimeRule
        {
            get { return _isOneTimeRule; }
            set
            {
                if (_isOneTimeRule == value) return;
                _isOneTimeRule = value;
                RaisePropertyChanged(nameof(IsOneTimeRule));
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
        #endregion

        #region commands
        #endregion
    }
}
